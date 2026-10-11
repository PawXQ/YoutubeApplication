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
        public ICommand SearchTypeCommand { get; set; }
        public ICommand VideoDurationCommand { get; set; }
        public ICommand VideoDefinitionCommand { get; set; }
        public ICommand VideoTypeCommand { get; set; }
        public ICommand PublishedAfterCommand { get; set; }

        public string QueryText { get; set; }
        public string SearchType { get; set; } = "video";
        public string VideoDuration { get; set; } = "any";
        public string VideoDefinition { get; set; } = "any";
        public string VideoType { get; set; } = "any";
        public string PublishedAfter { get; set; }


        public ObservableCollection<SearchVideoDTO> SearchVideos { get; set; }

        public MainViewModel()
        {
            this._videoSearchPresenter = new VideoSearchPresenter(this);

            this.ClickCommand = new RelayCommand(ClickTask, () => true);
            this.SearchTypeCommand = new RelayCommand<string>(SearchTypeClickTask, (parameter) => true);
            this.VideoDurationCommand = new RelayCommand<string>(VideoDurationClickTask, (parameter) => true);
            this.VideoDefinitionCommand = new RelayCommand<string>(VideoDefinitionClickTask, (parameter) => true);
            this.VideoTypeCommand = new RelayCommand<string>(VideoTypeClickTask, (parameter) => true);
            this.PublishedAfterCommand = new RelayCommand<string>(PublishedAfterClickTask, (parameter) => true);
        }

        public void ClickTask()
        {
            _videoSearchPresenter.VideoSearch(this.QueryText, this.PublishedAfter, this.VideoDuration, this.SearchType, this.VideoDefinition, this.VideoType);
        }

        public void SearchTypeClickTask(string parm)
        {
            this.SearchType = parm;
        }

        public void VideoDurationClickTask(string parm)
        {
            this.VideoDuration = parm;
        }
        public void VideoDefinitionClickTask(string parm)
        {
            this.VideoDuration = parm;
        }
        public void VideoTypeClickTask(string parm)
        {
            this.VideoDuration = parm;
        }
        public void PublishedAfterClickTask(string parm)
        {
            if (parm.ToString() == "today")
            {
                this.PublishedAfter = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                return;
            }
            if (parm.ToString() == "this_week")
            {
                this.PublishedAfter = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                return;
            }
            if (parm.ToString() == "this_month")
            {
                this.PublishedAfter = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                return;
            }
        }

        public void RenderVideoSearch(List<SearchVideoDTO> searchVideoDTOs)
        {
            this.SearchVideos = new ObservableCollection<SearchVideoDTO>(searchVideoDTOs);
        }
    }
}
