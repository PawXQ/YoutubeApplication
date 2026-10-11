using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApplication.Models;

namespace YoutubeApplication.Components.FilterComponent
{
    [AddINotifyPropertyChangedInterface]
    internal class FilterContext
    {
        public ObservableCollection<OptionItem> SearchTypeItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("Channel", "SearchType"),
             new OptionItem("playlist", "SearchType"),
             new OptionItem("video", "SearchType"),
        };

        public ObservableCollection<OptionItem> VideoDurationItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("short", "VideoDuration"),
             new OptionItem("long", "VideoDuration"),
        };

        public ObservableCollection<OptionItem> VideoDefinitionItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("high", "VideoDefinition"),
             new OptionItem("standard", "VideoDefinition"),
        };

        public ObservableCollection<OptionItem> VideoTypeItems { get; set; } = new ObservableCollection<OptionItem>()
        {
                new OptionItem("episode", "VideoType"),
                new OptionItem("movie", "VideoType")
        };

        public ObservableCollection<OptionItem> PublishedAfterItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("today", "PublishedAfter"),
             new OptionItem("this_week", "PublishedAfter"),
             new OptionItem("this_month", "PublishedAfter"),
        };
    }
}
