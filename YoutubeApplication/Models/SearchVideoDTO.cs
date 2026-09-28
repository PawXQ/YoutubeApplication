using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeApplication.Models
{
    internal class SearchVideoDTO
    {
        public string title { get; set; }
        public string channelTitle { get; set; }
        public string viewCount { get; set; }
        public string videoThumbnails { get; set; }
        public string channelThumbnails { get; set; }
        public string publishedAt { get; set; }
    }
}
