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
using Microsoft.UI.Composition;
using System.Numerics;
using WinRT;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            ExtendsContentIntoTitleBar = true;

            SetTitleBar(TitleBarElement);

            var GridElements = new List<InfoItemData>
            {
                new("Della Case", "128 Xaml Street", "Marketing"),
                new("John Garf", "64 Inventory Street", "Invetory"),
                new("Diana Smiley", "42 Search Street", "Reasearch"),
                new("Calis Decalis", "28 Ace Street", "Chemicals"),
                new("San Goku", "645 Fight Street", "Buisiness"),
                new("Harold Adams", "13 C# Street", "Developement"),
                new("Josh Skoter", "1 Relation Street", "Legal"),
                new("Christel Bloom", "34 John Mark Street", "Light"),
                new("Ema Dukerk", "7 Ice Street", "Navigation"),
                new("Della Case", "128 Xaml Street", "Marketing"),
                new("John Garf", "64 Inventory Street", "Invetory"),
                new("Diana Smiley", "42 Search Street", "Reasearch"),
                new("Calis Decalis", "28 Ace Street", "Chemicals"),
                new("San Goku", "645 Fight Street", "Buisiness"),
                new("Harold Adams", "13 C# Street", "Developement"),
                new("Josh Skoter", "1 Relation Street", "Legal"),
                new("Christel Bloom", "34 John Mark Street", "Light"),
                new("Ema Dukerk", "7 Ice Street", "Navigation"),
                new("Della Case", "128 Xaml Street", "Marketing"),
                new("John Garf", "64 Inventory Street", "Invetory"),
                new("Diana Smiley", "42 Search Street", "Reasearch"),
                new("Calis Decalis", "28 Ace Street", "Chemicals"),
                new("San Goku", "645 Fight Street", "Buisiness"),
                new("Harold Adams", "13 C# Street", "Developement"),
                new("Josh Skoter", "1 Relation Street", "Legal"),
                new("Christel Bloom", "34 John Mark Street", "Light"),
                new("Ema Dukerk", "7 Ice Street", "Navigation"),
                new("Christel Bloom", "34 John Mark Street", "Light"),
                new("Ema Dukerk", "7 Ice Street", "Navigation"),
                new("Della Case", "128 Xaml Street", "Marketing"),
            };

            InfoGridView.ItemsSource = GridElements;
        }

        private InfoItemData? _selectedData;

        private void InfoGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridViewItem? selectedItem = InfoGridView.ContainerFromItem(InfoGridView.SelectedItem) as GridViewItem;

            if (selectedItem is null) return;

            _selectedData = selectedItem.Content as InfoItemData;

            if (_selectedData is null) return;

            SelectedNameTextBlock.Text = _selectedData.Name;
            SelectedAddressTextBlock.Text = _selectedData.Address;
            SelectedJobTextBlock.Text = _selectedData.Job;
            
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
    }
}
