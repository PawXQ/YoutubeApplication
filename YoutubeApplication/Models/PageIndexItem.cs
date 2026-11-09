using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeApplication.Models
{
    [AddINotifyPropertyChangedInterface]
    internal class PageIndexItem
    {
        public bool IsChecked { get; set; }
        public string Title { get; set; }

        public PageIndexItem(string title) { this.Title = title; }
    }
}
