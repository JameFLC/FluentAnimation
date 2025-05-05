using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation.Controls
{
    public sealed partial class PlayItem : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PlayItem), new PropertyMetadata(null));


        public string Artist
        {
            get { return (string)GetValue(ArtistProperty); }
            set { SetValue(ArtistProperty, value); }
        }

        public static readonly DependencyProperty ArtistProperty =
            DependencyProperty.Register("Artist", typeof(string), typeof(PlayItem), new PropertyMetadata(null));


        public ImageSource Thumbnail
        {
            get { return (ImageSource)GetValue(ThumbnailProperty); }
            set { SetValue(ThumbnailProperty, value); }
        }

        public static readonly DependencyProperty ThumbnailProperty =
            DependencyProperty.Register("Thumbnail", typeof(ImageSource), typeof(PlayItem), new PropertyMetadata(null));

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register("IsPlaying", typeof(bool), typeof(PlayItem), new PropertyMetadata(null, IsPlayingChangedCallback));

        private bool _isHovered = false;
        private bool _isFocused = false;

        public PlayItem()
        {
            this.InitializeComponent();

            RootGrid.PointerEntered += HandlePointerEntered;
            RootGrid.PointerExited += HandlePointerExited;
            RootGrid.GettingFocus += HandleGettingFocus;
            RootGrid.LostFocus += HandleLostFocus;
            PointerEventHandler pointerEventHandler = new(HandleClick);
            RootGrid.AddHandler(PointerPressedEvent, pointerEventHandler, true);
            KeyEventHandler keyDownEventHandler = new(HandleKeyDown);
            RootGrid.AddHandler(KeyDownEvent, keyDownEventHandler, true);
        }

        private void HandleClick(object sender, PointerRoutedEventArgs e)
        {
            HanldeInteracted();
            //e.Handled = true;
        }

        private void HandleKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space)
            {
                HanldeInteracted();
            }
            //e.Handled = true;
        }

        private void HanldeInteracted()
        {
            IsPlaying = !IsPlaying;
        }

        private void HandlePointerEntered(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _isHovered = true;

            UpdatePlayIndicatorVisibility();
        }

        private void HandlePointerExited(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _isHovered = false;

            UpdatePlayIndicatorVisibility();
        }

        private void HandleGettingFocus(UIElement sender, Microsoft.UI.Xaml.Input.GettingFocusEventArgs args)
        {
            _isFocused = true;

            UpdatePlayIndicatorVisibility();
        }

        private void HandleLostFocus(object sender, RoutedEventArgs e)
        {
            _isFocused = false;

            UpdatePlayIndicatorVisibility();
        }

        private static void IsPlayingChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            PlayItem control = (PlayItem)obj;
            bool oldValue = (bool)e.OldValue;
            bool newValue = (bool)e.NewValue;

            control.UpdatePlayIndicatorVisibility();
        }

        private void UpdatePlayIndicatorVisibility()
        {
            bool isVisible = _isFocused || _isHovered || IsPlaying;

            if (isVisible)
            {
                VisualStateManager.GoToState(this, "PlayIndicatorVisible", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "PlayIndicatorHidden", true);
            }
        }
    }
}
