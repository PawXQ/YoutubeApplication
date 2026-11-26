using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YoutubeApplication.Utility
{
    internal interface INavigationService
    {
        void SetFrame(Frame frame);
        void Navigate(Type pageType);
        void Navigate(string pageName);
    }
}
