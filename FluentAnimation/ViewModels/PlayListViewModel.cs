using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.ViewModels
{
    internal partial class PlayListViewModel: ObservableObject
    {
        public PlayListViewModel(List<PlayItemViewModel> NewPlayList)
        {
            foreach (var item in NewPlayList)
            {
                PlayList.Add(item);
            }

            Playing = PlayList.FirstOrDefault();
        }

        [ObservableProperty]
        public partial ObservableCollection<PlayItemViewModel> PlayList { get; set; } = new();

        [ObservableProperty]
        public partial PlayItemViewModel? Playing { get; set; }
    }
}
