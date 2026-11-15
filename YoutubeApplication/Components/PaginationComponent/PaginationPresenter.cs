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

        int _totalItemsCount = 0; // 總資料 item 數量 (由外部傳入)
        int _countPerPage = 0;    // 每頁顯示的資料 item 數量 (由外部傳入)
        int _pagesPerBlock = 3;   // 下方每個 Block 顯示幾個頁碼按鈕 (可依需求改為固定值如 5、10，或於 SetCountPerPage 中同步設定)

        int _currentPaginationBlock = 1;

        // 1. 總頁數 = 總資料筆數 / 每頁資料筆數 (無條件進位)
        int _totalPageCount => (_countPerPage > 0 && _totalItemsCount > 0)
            ? (_totalItemsCount + _countPerPage - 1) / _countPerPage
            : 0;

        // 2. 總 Block 數 = 總頁數 / 每個 Block 顯示的頁碼數量 (無條件進位)
        int _totalBlockCount => (_pagesPerBlock > 0 && _totalPageCount > 0)
            ? (_totalPageCount + _pagesPerBlock - 1) / _pagesPerBlock
            : 0;

        public PaginationPresenter(IPaginationContext paginationContext)
        {
            this._paginationContext = paginationContext;
        }

        public void RenderPage(int num)
        {
            (int start, int end) = this._calculatePageRender(num);
            if (start == -1 && end == -1) return;

            if (num == -1)
            {
                // 往回切一個 Block 時，選中該 Block 的最後一個頁碼
                int lastIndexInBlock = end - start;
                this._renderPages(start, end, lastIndexInBlock);
                return;
            }
            this._renderPages(start, end);
        }

        private (int, int) _calculatePageRender(int num)
        {
            if (this._totalPageCount == 0) return (-1, -1);

            if (num == 1)
            {
                // 檢查是否已經是最後一個 Block
                if (this._currentPaginationBlock + 1 > this._totalBlockCount)
                {
                    return (-1, -1);
                }
                this._currentPaginationBlock++;
            }
            else if (num == -1)
            {
                // 檢查是否已經是第一個 Block
                if (this._currentPaginationBlock - 1 <= 0)
                {
                    return (-1, -1);
                }
                this._currentPaginationBlock--;
            }

            // 計算當前 Block 的起始頁碼與結束頁碼
            int start = (this._currentPaginationBlock - 1) * this._pagesPerBlock + 1;
            int end = Math.Min(start + this._pagesPerBlock - 1, this._totalPageCount);

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

            if (_pageIndexItems.Count > 0 && index >= 0 && index < _pageIndexItems.Count)
            {
                _pageIndexItems[index].IsChecked = true;
            }

            _paginationContext.RenderPages(this._pageIndexItems);
        }

        public void SelectPage(string title)
        {
            foreach (var item in _pageIndexItems)
            {
                item.IsChecked = false;
            }
            this._pageIndexItems.First(x => x.Title == title).IsChecked = true;
            _paginationContext.SelectedPageTitleCommand.Execute(title);
        }

        public void ChangePage(string str)
        {
            if (str == "Backward")
            {
                int _currentPageIndex = this._pageIndexItems.FindIndex(x => x.IsChecked == true);

                int _nextPageIndex = _currentPageIndex - 1;
                if (_nextPageIndex < 0)
                {
                    this.RenderPage(-1);
                    return;
                }

                string title = this._pageIndexItems[_nextPageIndex].Title;
                this.SelectPage(title);
            }
            else if (str == "Forward")
            {
                int _currentPageIndex = this._pageIndexItems.FindIndex(x => x.IsChecked == true);

                int _nextPageIndex = _currentPageIndex + 1;
                if (_nextPageIndex > this._pageIndexItems.Count - 1)
                {
                    this.RenderPage(1);
                    return;
                }

                string title = this._pageIndexItems[_nextPageIndex].Title;
                this.SelectPage(title);
            }
        }

        public void SetTotalPageCount(int totalItemsCount)
        {
            this._totalItemsCount = totalItemsCount;
            this._currentPaginationBlock = 1; // 重新設定總筆數時重置回第一個 Block
            this.TryRenderInitialPage();
        }

        public void SetCountPerPage(int countPerPage)
        {
            this._countPerPage = countPerPage;
            this._currentPaginationBlock = 1;
            this.TryRenderInitialPage();
        }

        private void TryRenderInitialPage()
        {
            if (this._totalItemsCount > 0 && this._countPerPage > 0)
            {
                this.RenderPage(0);
            }
        }
    }
}