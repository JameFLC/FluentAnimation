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

            BasicGridView.ItemsSource = GridElements;
        }
    }
}
