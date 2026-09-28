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
            Task VideoSearch(string query);
            Task<ResponseResult<GetVideoInfo>> VideoInfoSeardh(string id);
            Task<ResponseResult<GetChannelsInfo>> ChannelInfoSearch(string id);
        }

        internal interface IVideoSearchView
        {
            void RenderVideoSearch(List<SearchVideoDTO> searchVideoDTOs);
        }
    }
}
