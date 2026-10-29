using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeApplication.Models
{
    internal class SearchFilter
    {
        public string SearchType { get; set; }
        public string VideoDuration { get; set; }
        public string VideoDefinition { get; set; }
        public string VideoType { get; set; }
        private string _publishedAfter;
        public string PublishedAfter
        {
            get => _publishedAfter;
            set
            {
                switch (value)
                {
                    case "today":
                        _publishedAfter = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                        break;
                    case "this_week":
                        _publishedAfter = DateTime.UtcNow.AddDays(-7).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                        break;
                    case "this_month":
                        _publishedAfter = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
                        break;
                }
            }
        }
    }
}
