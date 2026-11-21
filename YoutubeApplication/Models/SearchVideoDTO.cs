using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeApi.Interface;
using YoutubeApplication.Presenter;
using YoutubeApplication.Utility;
using static YoutubeApplication.Contract.PlayListItemContract;

namespace YoutubeApplication.Models
{
    internal class SearchVideoDTO
    {
        static IPlayListItemPresenter _playListItemPresenter = new PlayListItemPresenter();

        public string title { get; set; }
        public string videoId { get; set; }
        public string channelTitle { get; set; }
        public string viewCount { get; set; }
        public string videoThumbnails { get; set; }
        public string channelThumbnails { get; set; }
        public string publishedAt { get; set; }
        public string duration { get; set; }

        public ICommand AddVideoItemPlayListCommand { get; set; }
        public ICommand RemoveVideoItemPlayListCommand { get; set; }

        public SearchVideoDTO()
        {
            AddVideoItemPlayListCommand = new RelayCommand<PlayListDTO>(playList =>
            {
                _playListItemPresenter.AddVideoToPlayList(playList.PlaylistId, this.videoId);
            }, (x) => true);

            RemoveVideoItemPlayListCommand = new RelayCommand<PlayListDTO>(playList =>
            {
                _playListItemPresenter.RemoveVideoFromPlayList(playList.PlaylistId, this.videoId);
            }, (x) => true);
        }
    }
}
