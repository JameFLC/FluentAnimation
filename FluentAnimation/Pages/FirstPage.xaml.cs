using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentAnimation.Models;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FirstPage : Page
    {
        public FirstPage()
        {
            this.InitializeComponent();

            var GridElements = new List<InfoItemData>
            {
                new("Della Case", "128 Xaml Street", "Marketing", 1+00),
                new("John Garf", "64 Inventory Street", "Invetory", 2+00),
                new("Diana Smiley", "42 Search Street", "Reasearch", 3+00),
                new("Calis Decalis", "28 Ace Street", "Chemicals", 4+100),
                new("San Goku", "645 Fight Street", "Buisiness", 5+00),
                new("Harold Adams", "13 C# Street", "Developement", 6+00),
                new("Josh Skoter", "1 Relation Street", "Legal", 7+00),
                new("Christel Bloom", "34 John Mark Street", "Light", 8+00),
                new("Ema Dukerk", "7 Ice Street", "Navigation", 9+100),
                new("Della Case", "128 Xaml Street", "Marketing", 10+00),
                new("John Garf", "64 Inventory Street", "Invetory", 11+00),
                new("Diana Smiley", "42 Search Street", "Reasearch", 12+00),
                new("Calis Decalis", "28 Ace Street", "Chemicals", 13+00),
                new("San Goku", "645 Fight Street", "Buisiness", 14+00),
                new("Harold Adams", "13 C# Street", "Developement", 15+00),
                new("Josh Skoter", "1 Relation Street", "Legal", 16+100),
                new("Christel Bloom", "34 John Mark Street", "Light", 17+00),
                new("Ema Dukerk", "7 Ice Street", "Navigation", 18+00),
                new("Della Case", "128 Xaml Street", "Marketing", 19+00),
                new("John Garf", "64 Inventory Street", "Invetory", 20+100),
                new("Diana Smiley", "42 Search Street", "Reasearch", 21+00),
                new("Calis Decalis", "28 Ace Street", "Chemicals", 22+00),
                new("San Goku", "645 Fight Street", "Buisiness", 23+00),
                new("Harold Adams", "13 C# Street", "Developement", 24+100),
                new("Josh Skoter", "1 Relation Street", "Legal", 25+00),
                new("Christel Bloom", "34 John Mark Street", "Light", 26+00),
                new("Ema Dukerk", "7 Ice Street", "Navigation", 27+00),
                new("Christel Bloom", "34 John Mark Street", "Light", 28+100),
                new("Ema Dukerk", "7 Ice Street", "Navigation", 29+00),
                new("Della Case", "128 Xaml Street", "Marketing", 30+00),
            };

            InfoGridView.ItemsSource = GridElements;
        }

        private InfoItemData? _selectedData;


        private void InfoGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridViewItem? selectedItem = InfoGridView.ContainerFromItem(e.ClickedItem) as GridViewItem;

            if (selectedItem is null) return;

            _selectedData = selectedItem.Content as InfoItemData;

            if (_selectedData is null) return;

            SelectedNameTextBlock.Text = _selectedData.Name;
            SelectedAddressTextBlock.Text = _selectedData.Address;
            SelectedJobTextBlock.Text = _selectedData.Job;
            SelectedImage.ImageSource = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(
                    new Uri($"https://picsum.photos/id/{_selectedData.PictureId}/200/300"));

            OverlayPopup.Visibility = Visibility.Visible;

            InfoGridView.SelectedItem = null;


            ConnectedAnimation connectedAnimation = InfoGridView.PrepareConnectedAnimation("forwardAnimation", _selectedData, "MainBorder");

            connectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
            connectedAnimation.TryStart(DestinationElement);
        }

        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ConnectedAnimation connectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", DestinationElement);

            connectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();

            await InfoGridView.TryStartConnectedAnimationAsync(connectedAnimation, _selectedData, "MainBorder");

            OverlayPopup.Visibility = Visibility.Collapsed;
        }

        private void InfoGridView_ItemClick_1(object sender, ItemClickEventArgs e)
        {

        }
    }
}
