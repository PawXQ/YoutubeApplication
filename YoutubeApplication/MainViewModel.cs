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
        public ICommand GetConfirmFilterCommand { get; set; }

        public string QueryText { get; set; }
        public string SearchType { get; set; } = "video";
        public string VideoDuration { get; set; } = "any";
        public string VideoDefinition { get; set; } = "any";
        public string VideoType { get; set; } = "any";
        public string PublishedAfter { get; set; }

        public SearchFilter SearchFilter { get; set; }

        public ObservableCollection<SearchVideoDTO> SearchVideos { get; set; }

        public ObservableCollection<PageIndexItem> PageIndices { get; set; } = new ObservableCollection<PageIndexItem>() { };

        public MainViewModel()
        {
            this._videoSearchPresenter = new VideoSearchPresenter(this);

            this.ClickCommand = new RelayCommand(ClickTask, () => true);

            this.GetConfirmFilterCommand = new RelayCommand<SearchFilter>((x) =>
            {
                this.SearchFilter = x;
            }, (x) => true);
        }

        public void ClickTask()
        {

            //_videoSearchPresenter.VideoSearch(this.QueryText, this.PublishedAfter, this.VideoDuration, this.SearchType, this.VideoDefinition, this.VideoType);


            _videoSearchPresenter.VideoSearch(
                 this.QueryText,
                 this.SearchFilter.PublishedAfter ?? this.PublishedAfter,
                 this.SearchFilter.VideoDuration ?? this.VideoDuration,
                 this.SearchFilter.SearchType ?? this.SearchType,
                 this.SearchFilter.VideoDefinition ?? this.VideoDefinition,
                 this.SearchFilter.VideoType ?? this.VideoType
             );
        }

        public SearchFilter GetConfirmFilterCommandTask(SearchFilter searchFilter)
        {
            return searchFilter;
        }

        public void RenderVideoSearch(List<SearchVideoDTO> searchVideoDTOs)
        {
            this.SearchVideos = new ObservableCollection<SearchVideoDTO>(searchVideoDTOs);
        }
    }
}
