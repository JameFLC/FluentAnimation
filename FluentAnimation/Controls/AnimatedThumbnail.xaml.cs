// Based on Animated image from AIDevGallery

using CommunityToolkit.WinUI.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;


namespace FluentAnimation.Controls
{
    internal partial class AnimatedThumbnail : UserControl
    {
        public static readonly DependencyProperty ThumbnailProperty = DependencyProperty.Register(
            nameof(Thumbnail),
            typeof(ImageSource),
            typeof(AnimatedThumbnail),
            new PropertyMetadata(defaultValue: null, (d, e) => ((AnimatedThumbnail)d).IsImageChanged((ImageSource)e.OldValue, (ImageSource)e.NewValue)));

        public ImageSource Thumbnail
        {
            get => (ImageSource)GetValue(ThumbnailProperty);
            set => SetValue(ThumbnailProperty, value);
        }

        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(TransitionDurationProperty); }
            set { SetValue(TransitionDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TransitionDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransitionDurationProperty =
            DependencyProperty.Register("TransitionDuration", typeof(TimeSpan), typeof(AnimatedThumbnail), new PropertyMetadata(DefaultDuration));

        public static readonly TimeSpan DefaultDuration = TimeSpan.FromMilliseconds(500);

        public AnimatedThumbnail()
        {
            this.InitializeComponent();
        }

        protected virtual void IsImageChanged(ImageSource oldValue, ImageSource newValue)
        {
            OnIsImageChanged();
        }

        private void OnIsImageChanged()
        {
            BottomImageBrush.ImageSource = Thumbnail;
            AnimationSet selectAnimation = [new OpacityAnimation() { From = 1, To = 0, Duration = AnimationDuration}];
            selectAnimation.Completed += (s, e) =>
            {
                TopImageBrush.ImageSource = Thumbnail;
                TopImage.Opacity = 1;
            };
            selectAnimation.Start(TopImage);
        }
    }
}
