using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeApplication.Models;

namespace YoutubeApplication.Components.PaginationComponent
{
    internal class PaginationContract
    {
        internal interface IPaginationPresenter
        {
            void SetTotalPageCount(int totalItemsCount);
            void SetCountPerPage(int countPerPage);
            void RenderPage(int num);
            void SelectPage(string title);
            void ChangePage(string str);
        }

        internal interface IPaginationContext
        {
            ICommand SelectedPageTitleCommand { get; set; }
            void RenderPages(List<PageIndexItem> pageIndexItems);
        }
    }
}
