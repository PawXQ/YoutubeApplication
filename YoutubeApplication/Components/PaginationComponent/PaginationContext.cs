using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeApplication.Models;
using YoutubeApplication.Presenter;
using YoutubeApplication.Utility;
using static YoutubeApplication.Components.PaginationComponent.PaginationContract;

namespace YoutubeApplication.Components.PaginationComponent
{
    [AddINotifyPropertyChangedInterface]
    internal class PaginationContext : IPaginationContext
    {
        IPaginationPresenter _paginationPresenter;
        public ObservableCollection<PageIndexItem> Pages { get; set; } = new ObservableCollection<PageIndexItem>();

        private int _totalPageCount;
        public int TotalPageCount
        {
            get => _totalPageCount;
            set
            {
                _totalPageCount = value;
                _paginationPresenter.SetTotalPageCount(value);
            }
        }
        private int _countPerPage;
        public int CountPerPage
        {
            get => _countPerPage;
            set
            {
                _countPerPage = value;
                _paginationPresenter.SetCountPerPage(value);
            }
        }

        public ICommand SelectPageCommand { get; set; }
        public ICommand ChangePageCommand { get; set; }

        public PaginationContext()
        {
            this._paginationPresenter = new PaginationPresenter(this);

            this.SelectPageCommand = new RelayCommand<PageIndexItem>(x => _paginationPresenter.SelectPage(x.Title), (x) => true);
            this.ChangePageCommand = new RelayCommand<string>(x => _paginationPresenter.ChangePage(x), (x) => true);
        }

        public void RenderPages(List<PageIndexItem> pageIndexItems)
        {
            Pages = new ObservableCollection<PageIndexItem>(pageIndexItems);
        }
    }
}
