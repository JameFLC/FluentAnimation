using System;
using System.Numerics;
using FluentAnimation.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation.Controls
{
    // TODO : Keyboard editing
    // TODO : FocusBox indicator like regular slider
    // TODO : Tooltip when editing


    [TemplatePart(Name = "Thumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "ContainerCanvas", Type = typeof(Canvas))]
    public partial class TriangleSlider : Control
    {

        private const double DefaultFirst = 1.0;
        private const double DefaultSecond = 0.0;
        private const double DefaultThird = 0.0;


        private Thumb? _thumb;
        private Canvas? _containerCanvas;
        private Border? _invisibleDrag;
        private Polygon? _triangle;
        private Grid? _toolTip;
        private TextBlock? _toolTipText;


        private Vector2 _absolutePosition = new();
        private bool _pointerManipulatingThumb;
        private float _canvaspPading = 4;

        static readonly float sqrt3 = MathF.Sqrt(3);
        private bool _valuesAssigned;
        private bool _valueUpdating;

        private Point _pointA = new();
        private Point _pointB = new();
        private Point _pointC = new();



        PointerEventHandler? _thumbPointerPressedHandler;

        public TriangleSlider()
        {
            DefaultStyleKey = typeof(TriangleSlider);

            _absolutePosition = new(0, float.PositiveInfinity);


            RefreshTriangle();
            RefreshFromMouseInput();
        }

        protected override void OnApplyTemplate()
        {
            if (_containerCanvas is not null)
            {
                _containerCanvas.SizeChanged -= ContainerCanvas_SizeChanged;
                _containerCanvas.PointerPressed -= ContainerCanvas_PointerPressed;
                _containerCanvas.PointerMoved -= ContainerCanvas_PointerMoved;
                _containerCanvas.PointerReleased -= ContainerCanvas_PointerReleased;
                _containerCanvas.PointerExited -= ContainerCanvas_PointerExited;
            }

            if (_thumb is not null)
            {
                _thumb.DragDelta -= Thumb_DragDelta;
                _thumb.DragCompleted -= Thumb_DragCompleted;
                _thumb.PointerPressed -= Thumb_PointerPressed;
                _thumb.DragStarted -= Thumb_DragStarted;
                if (_thumbPointerPressedHandler is not null)
                {
                    _thumb.RemoveHandler(PointerPressedEvent, _thumbPointerPressedHandler);
                }
            }

            _valuesAssigned = true;

            _containerCanvas = GetTemplateChild("ContainerCanvas") as Canvas;
            _thumb = GetTemplateChild("Thumb") as Thumb;
            _invisibleDrag = GetTemplateChild("InvisibleDrag") as Border;
            _triangle = GetTemplateChild("TrianglePath") as Polygon;
            _toolTip = GetTemplateChild("ToolTip") as Grid;
            _toolTipText = GetTemplateChild("ToolTipText") as TextBlock;

            if (_containerCanvas is not null)
            {
                _containerCanvas.SizeChanged += ContainerCanvas_SizeChanged;
                _containerCanvas.PointerEntered += ContainerCanvas_PointerEntered;
                _containerCanvas.PointerPressed += ContainerCanvas_PointerPressed;
                _containerCanvas.PointerMoved += ContainerCanvas_PointerMoved;
                _containerCanvas.PointerReleased += ContainerCanvas_PointerReleased;
                _containerCanvas.PointerExited += ContainerCanvas_PointerExited;
            }

            if (_thumb is not null)
            {
                _thumb.DragDelta += Thumb_DragDelta;
                _thumb.DragCompleted += Thumb_DragCompleted;
                _thumb.PointerPressed += Thumb_PointerPressed;
                _thumb.DragStarted += Thumb_DragStarted;
                _thumbPointerPressedHandler = new(Thumb_PointerPressed);
                _thumb.AddHandler(PointerPressedEvent, _thumbPointerPressedHandler, true);
            }
        }

        private Vector2 DragSize()
        {
            return _containerCanvas!.ActualSize;
        }

        private void ContainerCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshTriangle();

            RefreshFromMouseInput();
        }

        private void RefreshTriangle()
        {
            if (_triangle is null || _containerCanvas is null)
            {
                return;
            }

            var canvasSize = _containerCanvas.ActualSize;

            // Equilateral triangle altitude (h) formula
            // a = side length
            // h = (a * sqrt(3)) / 2
            float sideLenght = canvasSize.X;
            float altitude = (sideLenght * sqrt3 / 2f) - _canvaspPading;

            // Radius is the distance between the center point of the triangle and its top most point or the radius of the circle perfectly containing the triangle
            float radius = altitude * 2f / 3f;

            // Absolute X offset of the bottom points compared to the center of the triangle 
            float bottomsXOffset = radius * sqrt3 / 2f;
            // Y offset of the bottom points compared to the center of the triangle 
            float bottomsYOffset = radius / 2f;

            // If the triangle is placed in the center of the cube, how much does the triangle go beyond on the top of the canvas, we should offset it to not go beyond the canvas
            float triangleOvershoot = -(canvasSize.Y / 2 - radius);

            // The canvas is taller than the triangle, so if it is on the top of the canvs and we offset it half of the height difference it should be centered on the Y axis
            float triangleMargin = canvasSize.Y - altitude;

            // Offseted center point of the triangle
            Vector2 centerPoint = canvasSize / 2f + new Vector2(0, triangleOvershoot + triangleMargin / 2);

            //-----------------------------|
            //              B              |
            //             / \             |
            //            /   \            |
            //           /     \           |
            //          /       \          |
            //       G /         \ H       |
            //        /           \        |
            //       /             \       |
            //      /               \      |
            //     /                 \     |
            //    /                   \    |
            //   /_____________________\   |
            //  A           I           C  |
            //-----------------------------|

            _pointA = new Point(centerPoint.X - bottomsXOffset, centerPoint.Y + bottomsYOffset);
            _pointB = new Point(centerPoint.X, centerPoint.Y - radius);
            _pointC = new Point(centerPoint.X + bottomsXOffset, centerPoint.Y + bottomsYOffset);





            var trianglePoints = new PointCollection() { _pointA, _pointB, _pointC };

            _triangle.Points = trianglePoints;
        }

        private void RefreshFromMouseInput()
        {
            if (_containerCanvas is null || _thumb is null)
            {
                return;
            }

            Vector2 _boundAbsolutePosition = new Vector2
            {
                X = MathF.Max(0, MathF.Min(_containerCanvas.ActualSize.X, (float)_absolutePosition.X)),
                Y = MathF.Max(0, MathF.Min(_containerCanvas.ActualSize.Y, (float)_absolutePosition.Y))
            };

            UpdatePropertiesFromPosition(Vector2Ex.ToPoint(_boundAbsolutePosition));

            SyncThumb();
        }

        private Point ThumbProjectedTrianglePosition()
        {
            if (First == 0)
            {
                return PlacePointOnSegmentFromFactor(_pointB, _pointC, Third);
            }
            else if (Second == 0)
            {
                return PlacePointOnSegmentFromFactor(_pointA, _pointC, Third);
            }
            else
            {
                return PlacePointOnSegmentFromFactor(_pointA, _pointB, Second);
            }
        }

        private void SyncThumb()
        {
            if (_thumb is null)
            {
                return;
            }

            Point thumbPosition = ThumbProjectedTrianglePosition();

            Canvas.SetLeft(_thumb, thumbPosition.X - _thumb.ActualSize.X / 2f);
            Canvas.SetTop(_thumb, thumbPosition.Y - _thumb.ActualSize.Y / 2f);


            if (_invisibleDrag is not null)
            {
                Canvas.SetLeft(_invisibleDrag, _absolutePosition.X - _invisibleDrag.ActualSize.X / 2f);
                Canvas.SetTop(_invisibleDrag, _absolutePosition.Y - _invisibleDrag.ActualSize.Y / 2f);
            }

            SyncToolTip(thumbPosition);
        }


        private void UpdatePropertiesFromPosition(Point ThumbPosition)
        {
            //-----------------------------| Property "First" corresponds to the closeness to point A (in comparison with the two other points)
            //              B              |
            //             / \             | Property "Second" corresponds to the closeness to point B (in comparison with the two other points)
            //            /   \            |
            //           /     \           | Property "Third" corresponds to the closeness to point C (in comparison with the two other points)
            //          /       \          |
            //       G /         \ H       |
            //        /           \        |
            //       /             \       |
            //      /               \      |
            //     /                 \     |
            //    /                   \    |
            //   /_____________________\   |
            //  A           I           C  |
            //-----------------------------|

            Point thumbOnG = ProjectPointOnSegment(_pointA, _pointB, ThumbPosition);
            double distance2ToG = PointDistance(ThumbPosition, thumbOnG);

            Point thumbOnH = ProjectPointOnSegment(_pointB, _pointC, ThumbPosition);
            double distance2ToH = PointDistance(ThumbPosition, thumbOnH);

            Point thumbOnI = ProjectPointOnSegment(_pointC, _pointA, ThumbPosition);
            double distance2ToI = PointDistance(ThumbPosition, thumbOnI);


            if (distance2ToG <= distance2ToH && distance2ToG <= distance2ToI)
            {
                var SegmentFactor = GetSegmentFactor(_pointA, _pointB, ThumbPosition).Clamped(0, 1);

                First = 1 - SegmentFactor;
                Second = SegmentFactor;
                Third = 0;
            }
            else if (distance2ToH <= distance2ToI && distance2ToH <= distance2ToG)
            {
                var SegmentFactor = GetSegmentFactor(_pointB, _pointC, ThumbPosition).Clamped(0, 1);

                First = 0;
                Second = 1 - SegmentFactor;
                Third = SegmentFactor;
            }
            else
            {
                var SegmentFactor = GetSegmentFactor(_pointC, _pointA, ThumbPosition).Clamped(0, 1);

                First = SegmentFactor;
                Second = 0;
                Third = 1 - SegmentFactor;
            }
        }

        private double PointDistance(Point P1, Point P2)
        {
            double dx = P2.X - P1.X;
            double dy = P2.Y - P1.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        private Point ProjectPointOnSegment(Point P1, Point P2, Point PointToProjet)
        {
            double dx = P2.X - P1.X;
            double dy = P2.Y - P1.Y;

            double t = GetSegmentFactor(P1, P2, PointToProjet).Clamped(0, 1);

            // Construct the projected point P3' by moving t proportion along the vector from P1 to P2
            return new Point(P1.X + t * dx, P1.Y + t * dy);
        }

        private Point PlacePointOnSegmentFromFactor(Point P1, Point P2, double factor)
        {
            double dx = P2.X - P1.X;
            double dy = P2.Y - P1.Y;

            // Construct the projected point P3' by moving t proportion along the vector from P1 to P2
            return new Point(P1.X + factor * dx, P1.Y + factor * dy);
        }

        private double GetSegmentFactor(Point P1, Point P2, Point PointToProjet)
        {
            double dx = P2.X - P1.X;
            double dy = P2.Y - P1.Y;
            double lenSq = dx * dx + dy * dy;

            // This calculates the scalar value which represents how far along the line segment P1P2 
            // the projection of P3 falls. It is derived using the dot product of vectors.
            return ((PointToProjet.X - P1.X) * dx + (PointToProjet.Y - P1.Y) * dy) / lenSq;
        }

        private void ResetInvisibleDrag()
        {
            if (_invisibleDrag is not null && _containerCanvas is not null)
            {
                Canvas.SetLeft(_invisibleDrag, _containerCanvas.ActualWidth / 2 - _invisibleDrag.ActualWidth / 2f);
                Canvas.SetTop(_invisibleDrag, _containerCanvas.ActualHeight / 2 - _invisibleDrag.ActualHeight / 2f);
            }
        }

        private void SyncToolTip(Point thumbPosition)
        {
            if (_containerCanvas is null || _toolTip is null || _toolTipText is null)
            {
                return;
            }

            Vector2 canvasCenter = _containerCanvas.ActualSize / 2f;

            // The negative vector of the offset of the thumb compared to the center of the control (basically the inversed triangle)
            Vector2 projection = (thumbPosition.ToVector2() - canvasCenter);

            // Scale the projection to the aspect ratio of the tooltip
            Vector2 squishedProjection = projection * _toolTip.ActualSize;

            // We re-project the projections to a square (-1 1 in x and y)
            // This is the equivalent to slide this square in the direction of the projection so one of its sides is always touching a point at (0,0)

            // By using the squished projection, for the tooltip to stay on the sides of the thumb until we are very close to the top point
            Vector2 topProjection = GetSquareIntersection(squishedProjection);

            // Using the regular projection make the tooltip offsets itself correctly for the two bottom points since they are close
            // to the poisition of the bottom points of a square of the same size
            Vector2 bottomProjection = GetSquareIntersection(projection);

            bool inThumbOnTop = thumbPosition.Y < canvasCenter.Y;

            // Both projection are exactly equal when the thumb traverse the vertical center of the triangle, so we can switch them directly
            Vector2 finalSquareProjection = inThumbOnTop ? topProjection : bottomProjection;

            // Offset the tooltip in the direction of the final projection by 16epx
            Vector2 tooltipPadding = (finalSquareProjection * 16);

            Vector2 toolTipCenter = _toolTip.ActualSize / 2;

            // Local offset of the tooltip
            Vector2 toolTipLocalOffset = (toolTipCenter * finalSquareProjection) + tooltipPadding;
            
            Canvas.SetLeft(_toolTip, 0 + toolTipLocalOffset.X + thumbPosition.X - toolTipCenter.X);
            Canvas.SetTop(_toolTip, 0 + toolTipLocalOffset.Y + thumbPosition.Y - toolTipCenter.Y);

            _toolTipText.Text = $"{First:F2} | {Second:F2} | {Third:F2}";
        }

        private Vector2 GetSquareIntersection(Vector2 Direction, float SideLength = 2)
        {
            // Normalize the direction vector
            float magnitude = Direction.GetMagnitude();
            float unitX = Direction.X / magnitude;
            float unitY = Direction.Y / magnitude;

            // Find the scaling factor to reach the first intersection
            float scale = SideLength * 0.5f / Math.Max(Math.Abs(unitX), Math.Abs(unitY));

            // Calculate the intersection point
            float x = scale * unitX;
            float y = scale * unitY;

            return new(x, y);
        }
    }
}
