using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FluentAnimation.Controls
{
    public partial class TriangleSlider : Control
    {
        private void ContainerCanvas_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void ContainerCanvas_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void ContainerCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _absolutePosition = e.GetCurrentPoint(_containerCanvas).Position.ToVector2();

            _pointerManipulatingThumb = true;
            RefreshFromMouseInput();

            if (_toolTip is not null)
            {
                _toolTip.Opacity = 1;
            }
        }

        private void ContainerCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _pointerManipulatingThumb = false;

            ResetInvisibleDrag();

            if (_toolTip is not null)
            {
                _toolTip.Opacity = 0;
            }
        }

        private void ContainerCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (_pointerManipulatingThumb)
            {
                _absolutePosition = e.GetCurrentPoint(_containerCanvas).Position.ToVector2();


                RefreshFromMouseInput();
            }
        }
    }
}
