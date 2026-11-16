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
        public ICommand SelectedPageTitleCommand { get; set; }
        public ICommand GetPlayListsCommand { get; set; }
        public ICommand AddVideoItemPlayListCommand { get; set; }

        public string QueryText { get; set; }

        public int TotalItemsCount { get; set; }  // 總資料筆數
        public int CountPerPage { get; set; } = 11;  // 每頁資料筆數

        private string _selectedPageTitle = "1";


        public SearchFilter SearchFilter { get; set; } = new SearchFilter();

        public ObservableCollection<SearchVideoDTO> RenderSearchVideos { get; set; }
        private List<SearchVideoDTO> TotalSearchVideoDTOs { get; set; }

        public ObservableCollection<PlayListDTO> PlayListDTOs { get; set; }

        public MainViewModel()
        {
            this._videoSearchPresenter = new VideoSearchPresenter(this);

            this.ClickCommand = new RelayCommand(ClickTask, () => true);

            this.GetConfirmFilterCommand = new RelayCommand<SearchFilter>((x) =>
            {
                this.SearchFilter = x;
            }, (x) => true);

            this.SelectedPageTitleCommand = new RelayCommand<string>((x) =>
            {
                this._selectedPageTitle = x;

                int pageTitleNum = int.TryParse(this._selectedPageTitle, out int size) ? size : 10;
                List<SearchVideoDTO> CalculateSearchVideoDTOs = TotalSearchVideoDTOs.Skip((pageTitleNum - 1) * CountPerPage).Take(CountPerPage).ToList();
                this.RenderSearchVideos = new ObservableCollection<SearchVideoDTO>(CalculateSearchVideoDTOs);

            }, (x) => true);

            this.GetPlayListsCommand = new RelayCommand(() => _videoSearchPresenter.GetOwnPlayList(), () => true);
        }

        public void ClickTask()
        {
            _videoSearchPresenter.VideoSearch(
                 this.QueryText,
                 this.SearchFilter.PublishedAfter,
                 this.SearchFilter.VideoDuration,
                 this.SearchFilter.SearchType,
                 this.SearchFilter.VideoDefinition,
                 this.SearchFilter.VideoType
             );
        }

        public SearchFilter GetConfirmFilterCommandTask(SearchFilter searchFilter)
        {
            return searchFilter;
        }

        public void RenderVideoSearch(List<SearchVideoDTO> searchVideoDTOs)
        {
            this.TotalSearchVideoDTOs = searchVideoDTOs;

            int pageTitleNum = int.TryParse(this._selectedPageTitle, out int size) ? size : 10;
            List<SearchVideoDTO> CalculateSearchVideoDTOs = TotalSearchVideoDTOs.Skip((pageTitleNum - 1) * CountPerPage).Take(CountPerPage).ToList();
            this.RenderSearchVideos = new ObservableCollection<SearchVideoDTO>(CalculateSearchVideoDTOs);


            this.TotalItemsCount = searchVideoDTOs.Count;
        }

        public void RenderOwnPlayList(List<PlayListDTO> playListDTOs)
        {
            this.PlayListDTOs = new ObservableCollection<PlayListDTO>(playListDTOs);
        }
    }
}
