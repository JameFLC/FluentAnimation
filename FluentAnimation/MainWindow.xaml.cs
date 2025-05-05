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
using FluentAnimation.Models;
using FluentAnimation.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private int _currentSelectedPageIndex = 0;

        public MainWindow()
        {
            this.InitializeComponent();

            ExtendsContentIntoTitleBar = true;

            SetTitleBar(TitleBarElement);
        }

        private void PageSelector_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            int newlySelectedPageIndex = sender.Items.IndexOf(selectedItem);
            Type? pageType;

            switch (newlySelectedPageIndex)
            {
                case 0:
                    pageType = typeof(FirstPage);
                    break;
                case 1:
                    pageType = typeof(AdvancedAnimation);
                    break;
                case 2:
                    pageType = typeof(TriangleSliderPage);
                    break;
                case 3:
                    pageType = typeof(PlayingTransitionPage);
                    break;
                default:
                    pageType = typeof(FirstPage);
                    break;
            }

            if (pageType is null) return;

            var slideNavigationTransitionEffect = newlySelectedPageIndex - _currentSelectedPageIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

            MainFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });

            _currentSelectedPageIndex = newlySelectedPageIndex;

        }
    }
}
