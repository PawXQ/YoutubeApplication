using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApplication.Models;

namespace YoutubeApplication.Components.PaginationComponent
{
    internal class PaginationContract
    {
        internal interface IPaginationPresenter
        {
            void SetTotalPageCount(int totalPageCount);
            void SetCountPerPage(int countPerPage);
            void RenderPage(int num);
            void SelectPage(string title);
            void ChangePage(string str);
        }

        internal interface IPaginationContext
        {
            void RenderPages(List<PageIndexItem> pageIndexItems);
        }
    }
}
