using HttpUtility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi;
using YoutubeApi.Interface;
using YoutubeApi.Model;
using static YoutubeApplication.Contract.PlayListItemContract;

namespace YoutubeApplication.Presenter
{
    internal class PlayListItemPresenter : IPlayListItemPresenter
    {
        YoutubeContext youtubeContext;
        public PlayListItemPresenter()
        {
            this.youtubeContext = new YoutubeContext();
        }
        public Task<ResponseResult<AddVideoItem>> AddVideoToPlayList(string playlistId, string videoId)
        {
            return youtubeContext.PlayListItem.AddVideoItemAsync(playlistId, videoId); ;
        }
    }
}
