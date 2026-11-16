using HttpUtility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Model;
using YoutubeApplication.Models;

namespace YoutubeApplication.Contract
{
    internal class VideoSearchContract
    {
        internal interface IVideoSearchPresenter
        {
            Task VideoSearch(string query, string publishedAfter, string videoDuration = "any", string searchType = "any", string VideoDefinition = "any", string VideoType = "any");
            Task<ResponseResult<GetVideoInfo>> VideoInfoSeardh(string id);
            Task<ResponseResult<GetChannelsInfo>> ChannelInfoSearch(string id);
            Task GetOwnPlayList();
            Task AddVideoItemPlayList(string playlistId, string videoId);
        }

        internal interface IVideoSearchView
        {
            void RenderVideoSearch(List<SearchVideoDTO> searchVideoDTOs);
            void RenderOwnPlayList(List<PlayListDTO> playListDTOs);
        }
    }
}
