using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeApplication.Models
{
    internal class OptionItem
    {
        public bool IsChecked { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }

        public OptionItem(string title, string groupName) { this.Title = title; this.GroupName = groupName; }
    }
}
