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

namespace YoutubeApplication.Presenter
{
    internal class VideoSearchPresenter : IVideoSearchPresenter
    {
        IVideoSearchView _videoSearchView;
        YoutubeContext youtubeContext;

        public VideoSearchPresenter(IVideoSearchView videoSearchView)
        {
            this._videoSearchView = videoSearchView;

            this.youtubeContext = new YoutubeContext();
        }
        public async Task VideoSearch(string query)
        {
            ResponseResult<SearchVideo> responseResult = await youtubeContext.Search.SearchVideoAsync(query);

            this._videoSearchView.RenderVideoSearch(responseResult.Data.items);
        }
    }
}
