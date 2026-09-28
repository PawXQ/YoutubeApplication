using HttpUtility.Model;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeApi;
using YoutubeApi.Interface;
using YoutubeApi.Model;
using YoutubeApplication.Models;
using YoutubeApplication.Presenter;
using YoutubeApplication.Utility;
using static YoutubeApplication.Contract.VideoSearchContract;

namespace YoutubeApplication
{
    [AddINotifyPropertyChangedInterface]
    internal class MainViewModel : IVideoSearchView
    {
        IVideoSearchPresenter _videoSearchPresenter;
        public ICommand ClickCommand { get; set; }

        public string SampleText { get; set; }
        public ObservableCollection<SearchVideoDTO> SearchVideos { get; set; }

        public MainViewModel()
        {
            this._videoSearchPresenter = new VideoSearchPresenter(this);

            this.ClickCommand = new RelayCommand(ClickTask, () => true);
        }

        public void ClickTask()
        {
            _videoSearchPresenter.VideoSearch(this.SampleText);
        }

        public void RenderVideoSearch(List<SearchVideoDTO> searchVideoDTOs)
        {
            this.SearchVideos = new ObservableCollection<SearchVideoDTO>(searchVideoDTOs);
        }
    }
}
