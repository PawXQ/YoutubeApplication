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

        public Task<ResponseResult<GetPlayListVideoItemsInfo>> GetPlayListVideoItemInfo(string playlistId)
        {
            return youtubeContext.PlayListItem.GetPlayListVideoItemInfoAsync(playlistId);
        }

        public Task DeleteVideoFromPlayList(string id)
        {
            return youtubeContext.PlayListItem.DeleteVideoItemAsync(id);
        }

        public async void RemoveVideoFromPlayList(string playlistId, string videoId)
        {
            ResponseResult<GetPlayListVideoItemsInfo> playListVodeoItemsInfo = await GetPlayListVideoItemInfo(playlistId);

            List<string> strings = playListVodeoItemsInfo.Data.items.Select(x => x.snippet.resourceId.videoId).ToList();

            string playListVideoItemId = playListVodeoItemsInfo.Data.items.FirstOrDefault(x => x.snippet.resourceId.videoId == videoId)?.id;

            if (playListVideoItemId != null) await DeleteVideoFromPlayList(playListVideoItemId);
        }
    }
}
