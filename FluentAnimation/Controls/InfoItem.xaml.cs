using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using static System.Runtime.InteropServices.JavaScript.JSType;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation.Controls
{
    public sealed partial class InfoItem : UserControl
    {
        #region DependencyProperties

        public string FullName
        {
            get { return (string)GetValue(FullNameProperty); }
            set { SetValue(FullNameProperty, value); }
        }
        public static readonly DependencyProperty FullNameProperty =
            DependencyProperty.Register(
        "FullName",
                typeof(string),
                typeof(InfoItem),
                new PropertyMetadata(null));

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }
        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register(
        "Address",
                typeof(string),
                typeof(InfoItem),
                new PropertyMetadata(null));

        public string Job
        {
            get { return (string)GetValue(JobProperty); }
            set { SetValue(JobProperty, value); }
        }
        public static readonly DependencyProperty JobProperty =
            DependencyProperty.Register(
        "Job",
                typeof(string),
                typeof(InfoItem),
                new PropertyMetadata(null));

        public int PictureId
        {
            get { return (int)GetValue(PictureIdProperty); }
            set { SetValue(PictureIdProperty, value); }
        }
        public static readonly DependencyProperty PictureIdProperty =
            DependencyProperty.Register(
        "PictureId",
                typeof(int),
                typeof(InfoItem),
                new PropertyMetadata(0, OnPhotoIdChanged));

        #endregion
        public InfoItem()
        {
            this.InitializeComponent();
            this.PointerEntered += InfoItem_PointerEntered;
            this.PointerExited += InfoItem_PointerExited;
        }

        private void InfoItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "PointerOver", true);
        }

        private void InfoItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            BottomButtonFlyout.Hide();
        }

        private static void OnPhotoIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InfoItem control)
            {
                control.UpdateImageSource();
            }
        }

        private void UpdateImageSource()
        {
            if (ItemImageBrush != null)
            {
                Microsoft.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new(
                    new Uri($"https://picsum.photos/id/{PictureId}/200/300"));

                ItemImageBrush.ImageSource = bitmapImage;
                TooltipImage.ImageSource = bitmapImage;
            }
        }
    }
}
