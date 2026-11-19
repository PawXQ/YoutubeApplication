using HttpUtility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Model;

namespace YoutubeApplication.Contract
{
    internal class PlayListItemContract
    {
        internal interface IPlayListItemPresenter
        {
            Task<ResponseResult<AddVideoItem>> AddVideoToPlayList(string playlistId, string videoId);
        }
    }
}
