using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.Controls
{
    public partial class TriangleSlider : Control
    {
        private void Thumb_DragDelta(object sendr, DragDeltaEventArgs e)
        {
            _absolutePosition +=
                new Vector2(
                    (float)e.HorizontalChange,
                    (float)e.VerticalChange
                );

            RefreshFromMouseInput();
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (_toolTip is not null)
            {
                _toolTip.Opacity = 1;
            }
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            RefreshFromMouseInput();
            ResetInvisibleDrag();

            if (_toolTip is not null)
            {
                _toolTip.Opacity = 0;
            }
        }

        private void Thumb_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _absolutePosition = e.GetCurrentPoint(_containerCanvas).Position.ToVector2();
            RefreshFromMouseInput();
        }
    }
}
