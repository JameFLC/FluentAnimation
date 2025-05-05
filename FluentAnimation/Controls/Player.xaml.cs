using ColorThiefDotNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Linq;
using Windows.Graphics.Imaging;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Player : UserControl
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
            DependencyProperty.Register("Thumbnail", typeof(ImageSource), typeof(PlayItem), new PropertyMetadata(null, ThumbnailChangedCallback));

        public Player()
        {
            this.InitializeComponent();
        }

        private static async void ThumbnailChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Player control = (Player)obj;
            ImageSource oldValue = (ImageSource)e.OldValue;
            ImageSource newValue = (ImageSource)e.NewValue;

            if (control.Thumbnail is null) return;

            var colorthief = new ColorThief();

            System.Drawing.Bitmap? bitmap = await ImageSourceConverter.GetBitmapFromImageSource(control.Thumbnail);

            if (bitmap != null)
            {
                QuantizedColor color = colorthief.GetColor(bitmap, 10, false);
                var palette = colorthief.GetPalette(bitmap, 3, 10, false);

                //palette.OrderBy(c => c.Color);

                color = palette.First();

                SolidColorBrush brush = new SolidColorBrush(new() { R = color.Color.R, G = color.Color.G, B = color.Color.B, A = byte.MaxValue });

                control.ExpandedPlayerBackground.Background = brush;
            }
        }
    }
}
