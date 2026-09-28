using HttpUtility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Model;
using YoutubeApi;
using static YoutubeApplication.Contract.VideoSearchContract;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Concurrent;
using YoutubeApplication.Models;

namespace YoutubeApplication.Presenter
{
    internal class VideoSearchPresenter : IVideoSearchPresenter
    {
        IVideoSearchView _videoSearchView;
        YoutubeContext youtubeContext;

        private ConcurrentBag<SearchVideoDTO> searchVideoDTOs = new ConcurrentBag<SearchVideoDTO>();

        public VideoSearchPresenter(IVideoSearchView videoSearchView)
        {
            this._videoSearchView = videoSearchView;

            this.youtubeContext = new YoutubeContext();
        }

        public async Task VideoSearch(string query)
        {
            ResponseResult<SearchVideo> responseResult = await youtubeContext.Search.SearchVideoAsync(query);

            foreach (var item in responseResult.Data.items)
            {
                SearchVideoDTO searchVideoDTO = new SearchVideoDTO();

                searchVideoDTO.title = item.snippet.title;
                searchVideoDTO.channelTitle = item.snippet.channelTitle;
                searchVideoDTO.publishedAt = item.snippet.publishedAt.ToString();
                searchVideoDTO.videoThumbnails = item.snippet.thumbnails.medium.url;

                string videoId = item.id.videoId;
                string channelId = item.snippet.channelId;

                ResponseResult<GetVideoInfo> getVideoInfoResult = await VideoInfoSeardh(videoId);
                ResponseResult<GetChannelsInfo> getChannelsInfoResult = await ChannelInfoSearch(channelId);

                searchVideoDTO.viewCount = getVideoInfoResult.Data.items[0].statistics.viewCount;
                searchVideoDTO.channelThumbnails = getChannelsInfoResult.Data.items[0].snippet.thumbnails.medium.url;

                searchVideoDTOs.Add(searchVideoDTO);
            }


            this.RenderVideoSearch();
        }

        public async Task<ResponseResult<GetVideoInfo>> VideoInfoSeardh(string id)
        {
            ResponseResult<GetVideoInfo> responseResult = await youtubeContext.Video.GetVideoInfoAsync(id);

            return responseResult;
        }

        public async Task<ResponseResult<GetChannelsInfo>> ChannelInfoSearch(string id)
        {
            ResponseResult<GetChannelsInfo> responseResult = await youtubeContext.Channels.GetChannelsInfoAsync(id);

            return responseResult;
        }

        public void RenderVideoSearch()
        {
            this._videoSearchView.RenderVideoSearch(searchVideoDTOs.ToList());
        }
    }
}
