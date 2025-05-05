using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.ViewModels
{
    internal partial class PlayItemViewModel(string title, string artist, ImageSource thumbnail, bool isPlaying = false) : ObservableObject
    {
        [ObservableProperty]
        public partial string Title { get; set; } = title;

        [ObservableProperty]
        public partial string Artist { get; set; } = artist;

        [ObservableProperty]
        public partial ImageSource Thumbnail { get; set; } = thumbnail;

        [ObservableProperty]
        public partial bool IsPlaying { get; set; } = isPlaying;
    }
}
