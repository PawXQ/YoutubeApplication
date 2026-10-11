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
using System.Diagnostics;
using System.Threading;
using YoutubeApi.Service;

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

        public async Task VideoSearch(string query, string publishedAfter, string videoDuration = "any", string searchType = "video", string VideoDefinition = "any", string VideoType = "any")
        {
            ResponseResult<SearchVideo> responseResult = await youtubeContext.Search.SearchVideoAsync(query, publishedAfter, videoDuration, searchType, VideoDefinition, VideoType);

            string itemsIdStr = string.Join(",", responseResult.Data.items.Select(x => x.id.videoId));
            string itemsChannelIdStr = string.Join(",", responseResult.Data.items.Select(x => x.snippet.channelId));

            List<Task> ItemTask = new List<Task>();

            Task<ResponseResult<GetVideoInfo>> getVideoInfoResultTask = VideoInfoSeardh(itemsIdStr);
            Task<ResponseResult<GetChannelsInfo>> getChannelsInfoResultTask = ChannelInfoSearch(itemsChannelIdStr);

            ItemTask.Add(getVideoInfoResultTask);
            ItemTask.Add(getChannelsInfoResultTask);
            await Task.WhenAll(ItemTask);

            ResponseResult<GetVideoInfo> getVideoInfoResult = await getVideoInfoResultTask;
            ResponseResult<GetChannelsInfo> getChannelsInfoResult = await getChannelsInfoResultTask;

            foreach (var item in responseResult.Data.items)
            {
                SearchVideoDTO searchVideoDTO = new SearchVideoDTO();

                searchVideoDTO.title = item.snippet.title;
                searchVideoDTO.channelTitle = item.snippet.channelTitle;
                searchVideoDTO.publishedAt = item.snippet.publishedAt.ToString();
                searchVideoDTO.videoThumbnails = item.snippet.thumbnails.medium.url;

                //searchVideoDTO.viewCount = getVideoInfoResult.Data.items.FirstOrDefault(x => x.id == item.id.videoId).statistics.viewCount;
                searchVideoDTO.viewCount = getVideoInfoResult.Data.items.FirstOrDefault(x => x.id == item.id.videoId)?.statistics?.viewCount ?? "0";
                //searchVideoDTO.duration = getVideoInfoResult.Data.items.FirstOrDefault(x => x.id == item.id.videoId).contentDetails.duration;
                searchVideoDTO.duration = getVideoInfoResult.Data.items.FirstOrDefault(x => x.id == item.id.videoId)?.contentDetails?.duration ?? "0";
                //searchVideoDTO.channelThumbnails = getChannelsInfoResult.Data.items.FirstOrDefault(x => x.id == item.snippet.channelId).snippet.thumbnails.medium.url;
                searchVideoDTO.channelThumbnails = getChannelsInfoResult.Data.items.FirstOrDefault(x => x.id == item.snippet.channelId)?.snippet?.thumbnails?.medium?.url ?? "0";

                searchVideoDTOs.Add(searchVideoDTO);
            }

            this.RenderVideoSearch();
        }

        public Task<ResponseResult<GetVideoInfo>> VideoInfoSeardh(string id)
        {
            return youtubeContext.Video.GetVideoInfoAsync(id); ;
        }

        public Task<ResponseResult<GetChannelsInfo>> ChannelInfoSearch(string id)
        {
            return youtubeContext.Channels.GetChannelsInfoAsync(id);
        }

        public void RenderVideoSearch()
        {
            this._videoSearchView.RenderVideoSearch(searchVideoDTOs.ToList());
        }
    }
}
