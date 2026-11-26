using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YoutubeApplication.Utility
{
    internal class NavigationService : INavigationService
    {
        Frame frame;
        public void Navigate(Type pageType)
        {

            Page page = (Page)Activator.CreateInstance(pageType);
            frame.Navigate(page);
        }

        public void Navigate(string pageName)
        {
            //if (pageName == "Home")
            //{
            //    frame.Navigate(new Home());
            //}
            //else
            //{
            //    frame.Navigate(new About());
            //}
        }

        public void SetFrame(Frame frame)
        {
            this.frame = frame;
        }
    }
}
