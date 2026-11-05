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
        public ICommand ChangePageIndexCommand { get; set; }

        public string QueryText { get; set; }
        public string SearchType { get; set; } = "video";
        public string VideoDuration { get; set; } = "any";
        public string VideoDefinition { get; set; } = "any";
        public string VideoType { get; set; } = "any";
        public string PublishedAfter { get; set; }

        private int RowIndex = 12;
        private int BatchQuantity = 10;
        private int Batch;
        private int PageRemainder;

        private int _currentPageIndex = 0;
        public int CurrentPageIndex
        {
            get => _currentPageIndex;
            set
            {
                this._currentPageIndex = value;
            }
        }
        public SearchFilter SearchFilter { get; set; }

        public ObservableCollection<SearchVideoDTO> SearchVideos { get; set; }

        public ObservableCollection<PageIndexItem> PageIndices { get; set; } = new ObservableCollection<PageIndexItem>() { };

        public MainViewModel()
        {
            this.Batch = this.RowIndex % this.BatchQuantity == 0 ? RowIndex / BatchQuantity : RowIndex / BatchQuantity + 1;
            this.PageRemainder = this.RowIndex % this.BatchQuantity;

            //for (int i = 0; i < 10; i++)
            //{
            //    PageIndexItem pageIndexItem = new PageIndexItem((i + 1).ToString());
            //    if (i == 0) pageIndexItem.IsChecked = true;
            //    PageIndices.Add(pageIndexItem);
            //}
            RenderPageIndices(1);

            this._videoSearchPresenter = new VideoSearchPresenter(this);

            this.ClickCommand = new RelayCommand(ClickTask, () => true);

            this.GetConfirmFilterCommand = new RelayCommand<SearchFilter>((x) =>
            {
                this.SearchFilter = x;
            }, (x) => true);

            this.ChangePageIndexCommand = new RelayCommand<string>(ChangePageIndexTask, (x) => true);
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
        public void ChangePageIndexTask(string str)
        {
            foreach (PageIndexItem pageIndexItem in this.PageIndices)
            {
                pageIndexItem.IsChecked = false;
            }

            if (int.TryParse(str, out int result))
            {
                this._currentPageIndex = (result - 1) % this.BatchQuantity;

                this.PageIndices[this._currentPageIndex].IsChecked = true;

                Console.WriteLine(this.CurrentPageIndex);
                return;
            }
            else
            {
                int firstNum = int.Parse(this.PageIndices[0].Title);
                int currnetNum = int.Parse(this.PageIndices[this._currentPageIndex].Title);
                int lastNum = int.Parse(this.PageIndices.Last().Title);


                if (str == "gt" && currnetNum <= RowIndex)
                {
                    currnetNum++;
                    if (currnetNum <= lastNum)
                    {
                        this._currentPageIndex++;
                        this.PageIndices[this._currentPageIndex].IsChecked = true;
                    }
                    else if (currnetNum > RowIndex)
                    {
                        this.PageIndices[this._currentPageIndex].IsChecked = true;
                        return;
                    }
                    else
                    {
                        ChangePage(int.Parse(this.PageIndices[this._currentPageIndex].Title) + 1);
                    }
                }
                else if (str == "lt" && currnetNum > 0)
                {
                    currnetNum--;
                    if (currnetNum > firstNum)
                    {
                        this._currentPageIndex--;
                        this.PageIndices[this._currentPageIndex].IsChecked = true;
                    }
                    else if (currnetNum == 0)
                    {
                        this.PageIndices[this._currentPageIndex].IsChecked = true;
                        return;
                    }
                    else
                    {
                        ChangePage(int.Parse(this.PageIndices[this._currentPageIndex].Title) - 1);
                    }
                }

                Console.WriteLine(this.CurrentPageIndex);
                return;
            }
        }

        public void RenderPageIndices(int num)
        {
            if (num > this.RowIndex) return;

            int renderCount = this.BatchQuantity;

            int currentBatch = num / BatchQuantity + 1;

            if (currentBatch == this.Batch && this.PageRemainder != 0) renderCount = this.PageRemainder;

            this.PageIndices.Clear();
            for (int i = 0; i < renderCount; i++)
            {
                PageIndexItem pageIndexItem = new PageIndexItem((i + num).ToString());
                if (i == 0) pageIndexItem.IsChecked = true;
                PageIndices.Add(pageIndexItem);
            }
            this.CurrentPageIndex = 0;
        }

        private void ChangePage(int num)
        {
            if (num > int.Parse(this.PageIndices.Last().Title)) RenderPageIndices(num);
            else if (num < int.Parse(this.PageIndices.First().Title)) RenderPageIndices(num);
        }

        public void RenderVideoSearch(List<SearchVideoDTO> searchVideoDTOs)
        {
            this.SearchVideos = new ObservableCollection<SearchVideoDTO>(searchVideoDTOs);
        }
    }
}
