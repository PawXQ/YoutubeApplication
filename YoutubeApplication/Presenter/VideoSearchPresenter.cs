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

        public async Task VideoSearch(string query)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ResponseResult<SearchVideo> responseResult = await youtubeContext.Search.SearchVideoAsync(query);

            List<Task> tasks = responseResult.Data.items.Select(async item =>
            {
                List<Task> sepItemTask = new List<Task>();

                SearchVideoDTO searchVideoDTO = new SearchVideoDTO();

                searchVideoDTO.title = item.snippet.title;
                searchVideoDTO.channelTitle = item.snippet.channelTitle;
                searchVideoDTO.publishedAt = item.snippet.publishedAt.ToString();
                searchVideoDTO.videoThumbnails = item.snippet.thumbnails.medium.url;

                string videoId = item.id.videoId;
                string channelId = item.snippet.channelId;

                Task<ResponseResult<GetVideoInfo>> getVideoInfoResultTask = VideoInfoSeardh(videoId);
                Task<ResponseResult<GetChannelsInfo>> getChannelsInfoResultTask = ChannelInfoSearch(channelId);

                sepItemTask.Add(getVideoInfoResultTask);
                sepItemTask.Add(getChannelsInfoResultTask);
                await Task.WhenAll(sepItemTask);

                ResponseResult<GetVideoInfo> getVideoInfoResult = await getVideoInfoResultTask;
                ResponseResult<GetChannelsInfo> getChannelsInfoResult = await getChannelsInfoResultTask;

                searchVideoDTO.viewCount = getVideoInfoResult.Data.items[0].statistics.viewCount;
                searchVideoDTO.duration = getVideoInfoResult.Data.items[0].contentDetails.duration;
                searchVideoDTO.channelThumbnails = getChannelsInfoResult.Data.items[0].snippet.thumbnails.medium.url;

                searchVideoDTOs.Add(searchVideoDTO);
            }).ToList();

            await Task.WhenAll(tasks);

            this.RenderVideoSearch();

            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds / 1000.0}s");
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
