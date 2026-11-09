using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApplication.Models;
using static YoutubeApplication.Components.PaginationComponent.PaginationContract;

namespace YoutubeApplication.Components.PaginationComponent
{
    internal class PaginationPresenter : IPaginationPresenter
    {
        IPaginationContext _paginationContext;
        List<PageIndexItem> _pageIndexItems = new List<PageIndexItem>();

        int _totalPageCount = 0;
        int _countPerPage = 0;
        int _currentPaginationBlock = 1;
        int _totalPage => (_countPerPage > 0) ? (_totalPageCount + _countPerPage - 1) / _countPerPage : 0;
        int _reminderPage => this._totalPageCount % _countPerPage == 0 ? 0 : this._totalPageCount % _countPerPage;

        public PaginationPresenter(IPaginationContext paginationContext)
        {
            this._paginationContext = paginationContext;

            //this._totalPage = this._totalPageCount % this._countPerPage == 0 ? this._totalPageCount / this._countPerPage : (this._totalPageCount / this._countPerPage) + 1;
            //this._reminderPage = this._totalPageCount % _countPerPage == 0 ? 0 : this._totalPageCount % _countPerPage;
        }

        public void RenderPage(int num)
        {
            (int start, int end) = this._calculatePageRender(num);
            if (start == -1 && end == -1) return;

            if (num == -1)
            {
                this._renderPages(start, end, this._countPerPage - 1);
                return;
            }
            this._renderPages(start, end);
        }

        private (int, int) _calculatePageRender(int num)
        {
            int start = 0;
            int end = 0;
            if (num == 0)
            {
                start = this._currentPaginationBlock * this._countPerPage + 1 - this._countPerPage;
                end = start + this._countPerPage < this._totalPageCount ? start + this._countPerPage - 1 : start + this._reminderPage - 1;
            }
            else if (num == 1)
            {
                if (this._currentPaginationBlock + 1 > this._totalPage)
                {
                    start = -1;
                    end = -1;
                    return (start, end);
                };

                start = (this._currentPaginationBlock + 1) * this._countPerPage + 1 - this._countPerPage;
                end = start + this._countPerPage - 1 <= this._totalPageCount ? start + this._countPerPage - 1 : start + this._reminderPage - 1;
                this._currentPaginationBlock++;
            }
            else if (num == -1)
            {
                if (this._currentPaginationBlock - 1 == 0)
                {
                    start = -1;
                    end = -1;
                    return (start, end);
                };

                start = (this._currentPaginationBlock - 1) * this._countPerPage + 1 - this._countPerPage;
                end = start + this._countPerPage - 1;
                this._currentPaginationBlock--;
            }

            return (start, end);
        }

        private void _renderPages(int start, int end, int index = 0)
        {
            _pageIndexItems.Clear();
            for (int i = start; i <= end; i++)
            {
                PageIndexItem pageIndexItem = new PageIndexItem(i.ToString());
                _pageIndexItems.Add(pageIndexItem);
            }
            _pageIndexItems[index].IsChecked = true;
            _paginationContext.RenderPages(this._pageIndexItems);
        }

        public void SelectPage(string title)
        {
            foreach (var item in _pageIndexItems)
            {
                item.IsChecked = false;
            }
            this._pageIndexItems.First(x => x.Title == title).IsChecked = true;
        }

        public void ChangePage(string str)
        {
            if (str == "Backward")
            {
                int _currentPageIndex = this._pageIndexItems.FindIndex(x => x.IsChecked == true);

                int _nextPageIndex = _currentPageIndex - 1;
                if (_nextPageIndex < 0) { this.RenderPage(-1); return; };

                string title = this._pageIndexItems[_currentPageIndex - 1].Title;
                this.SelectPage(title);
            }
            else if (str == "Forward")
            {
                int _currentPageIndex = this._pageIndexItems.FindIndex(x => x.IsChecked == true);

                int _nextPageIndex = _currentPageIndex + 1;
                if (_nextPageIndex > this._pageIndexItems.Count - 1) { this.RenderPage(1); return; };

                string title = this._pageIndexItems[_currentPageIndex + 1].Title;
                this.SelectPage(title);
            }
        }

        public void SetTotalPageCount(int totalPageCount)
        {
            this._totalPageCount = totalPageCount;
            this.TryRenderInitialPage();
        }

        public void SetCountPerPage(int countPerPage)
        {
            this._countPerPage = countPerPage;
            this.TryRenderInitialPage();
        }

        private void TryRenderInitialPage()
        {
            if (this._totalPageCount > 0 && this._countPerPage > 0)
            {
                this.RenderPage(0);
            }
        }
    }
}
