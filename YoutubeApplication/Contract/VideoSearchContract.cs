using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Model;

namespace YoutubeApplication.Contract
{
    internal class VideoSearchContract
    {
        internal interface IVideoSearchPresenter
        {
            Task VideoSearch(string query);
        }

        internal interface IVideoSearchView
        {
            void RenderVideoSearch(SearchVideo.Item[] searchVideoItem);
        }
    }
}
