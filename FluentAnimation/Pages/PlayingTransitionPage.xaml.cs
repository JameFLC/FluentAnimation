using CommunityToolkit.Mvvm.ComponentModel;
using FluentAnimation.Controls;
using FluentAnimation.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentAnimation.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayingTransitionPage : Page
    {
        private PlayListViewModel _viewModel { get; set; }

        public PlayingTransitionPage()
        {
            this.InitializeComponent();

            _viewModel = new PlayListViewModel(
                [
                    new PlayItemViewModel("Did You Just Play Some Light Jazz", "Q_the_Trumpet", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-x7E8gSJPwc5XwZ3M-VzMjzQ-t200x200.jpg")), true),
                    new PlayItemViewModel("change", "Ryce", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-DMy21JuNRpuyT1nd-JOmhDw-t200x200.jpg"))),
                    new PlayItemViewModel("ETERNAL", "RYCE", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-Ei3GJEzlQzEAFFFy-h5V39g-t200x200.png"))),
                    new PlayItemViewModel("love again", "Ryce", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-vzLBgS3H1fc5Woq5-ZCEzZQ-t200x200.jpg"))),
                    new PlayItemViewModel("dreams", "Ryce", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-eCxNlYYZJws5zNtk-W3K6fA-t200x200.jpg"))),
                    new PlayItemViewModel("Daybreak", "Anomalie", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-000228762610-d606d8-t200x200.jpg"))),
                    new PlayItemViewModel("Perro Funky - CLOSE YOUR EYES", "AwGeezRick", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-000344237853-zofpin-t200x200.jpg"))),
                    new PlayItemViewModel("front porch view [feat. liam compton]", "altitude.", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-000243456878-8i0u29-t200x200.jpg"))),
                    new PlayItemViewModel("Snow day! (ft. Joey Boone)", "Ice Cream Cult", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-000453879912-j4ist8-t200x200.jpg"))),
                    new PlayItemViewModel("Floating", "Qoopr", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-000246001981-po2pjq-t200x200.jpg"))),
                    new PlayItemViewModel("Conexão", "Sabrina Malheiros", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-fwOYkY87T2rj-0-t200x200.jpg"))),
                    new PlayItemViewModel("Everything In Its Right Place", "Radiohead", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-MzfZ45Pu9uLn-0-t200x200.png"))),
                    new PlayItemViewModel("Leiria (feat. Mael)", "Anomalie featuring Mael", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-mXpIoXNONAsV-0-t200x200.png"))),
                    new PlayItemViewModel("Too Soon You're Old", "Penny Goodwin", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-60Rty401zmwB-0-t200x200.jpg"))),
                    new PlayItemViewModel("Strawberry Milkshake", "SOFY", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-d97K4jSypoP6zsre-zhUlyQ-t200x200.jpg"))),
                    new PlayItemViewModel("What Do You Believe In?", "Rag'n'Bone Man", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-pU06IGhfvEpv-0-t200x200.jpg"))),
                    new PlayItemViewModel("lemonade w/ marc indigo (prod. by Noden)", "zamir", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-PKrzFRECZfaQNd9M-g5CtfQ-t200x200.jpg"))),
                    new PlayItemViewModel("A Perfect Date... To Die For (prod. Garrett.)", "Headhaunter", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-uByHcOc2wTblLg0N-rzPhow-t200x200.jpg"))),
                    new PlayItemViewModel("butterflies", "Samsa", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-000213217885-6u6n6y-t200x200.jpg"))),
                    new PlayItemViewModel("12:01am", "Seycara Orchestral", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-kQa0wMBfpBZe-0-t200x200.jpg"))),
                    new PlayItemViewModel("After The Storm (feat. Tyler, The Creator Bootsy Collins)", "Kali Uchis", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-ZFCyfGAxokhR-0-t200x200.jpg"))),
                    new PlayItemViewModel("Moonlight", "Kali Uchis", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-LRl2vujgUlA2-0-t200x200.jpg"))),
                    new PlayItemViewModel("telepatía", "Kali Uchis", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-Or5mfGi3B5po-0-t200x200.jpg"))),
                    new PlayItemViewModel("Giant", "Calvin Harris, Rag'n'Bone Man", new BitmapImage(new Uri("https://i1.sndcdn.com/artworks-wB3TGbJ586n9-0-t200x200.jpg")))
                ]
            );

            _viewModel.PropertyChanging += Playing_PropertyChanging;
            _viewModel.PropertyChanged += Playing_PropertyChanged;

            _viewModel.Playing = _viewModel.PlayList.First();

        }

        private void Playing_PropertyChanging(object? sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Playing) && _viewModel.Playing is not null)
            {
                _viewModel.Playing.IsPlaying = false;
            }
        }

        private void Playing_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Playing) && _viewModel.Playing is not null)
            {
                _viewModel.Playing.IsPlaying = true;
                UpdatePlayer();
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as PlayItemViewModel;
            if (clickedItem is null) return;

            _viewModel.Playing = clickedItem;
        }

        private void UpdatePlayer()
        {
            if (_viewModel.Playing is null)
            {
                return;
            }
            PagePlayer.Title = _viewModel.Playing.Title;
            PagePlayer.Artist = _viewModel.Playing.Artist;
            PagePlayer.Thumbnail = _viewModel.Playing.Thumbnail;
        }
    }
}
