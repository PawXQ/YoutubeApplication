using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YoutubeApplication.Components.PaginationComponent
{
    /// <summary>
    /// Interaction logic for Pagination.xaml
    /// </summary>
    public partial class Pagination : UserControl
    {
        public Pagination()
        {
            InitializeComponent();
            this.DataContext = new PaginationContext();
        }

        public static readonly DependencyProperty TotalItemsCountProperty = DependencyProperty.Register(
        nameof(TotalItemsCount),
        typeof(int),
        typeof(Pagination),
        new PropertyMetadata(default(int), OnTotalItemsCountChanged, CoerceTotalItemsCount));
        public int TotalItemsCount
        {
            get => (int)GetValue(TotalItemsCountProperty);
            set => SetValue(TotalItemsCountProperty, value);
        }
        private static void OnTotalItemsCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pagination pagination = (Pagination)d;
            PaginationContext paginationContext = pagination.DataContext as PaginationContext;
            paginationContext.TotalItemsCount = pagination.TotalItemsCount;
        }
        private static object CoerceTotalItemsCount(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        public static readonly DependencyProperty CountPerPageProperty = DependencyProperty.Register(
        nameof(CountPerPage),
        typeof(int),
        typeof(Pagination),
        new PropertyMetadata(default(int), OnCountPerPageChanged, CoerceCountPerPage));
        public int CountPerPage
        {
            get => (int)GetValue(CountPerPageProperty);
            set => SetValue(CountPerPageProperty, value);
        }
        private static void OnCountPerPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pagination pagination = (Pagination)d;
            PaginationContext paginationContext = pagination.pagination.DataContext as PaginationContext;
            paginationContext.CountPerPage = pagination.CountPerPage;
        }
        private static object CoerceCountPerPage(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        public static readonly DependencyProperty SelectedPageTitleCommandProperty = DependencyProperty.Register(
        nameof(SelectedPageTitleCommand),
        typeof(ICommand),
        typeof(Pagination),
        new PropertyMetadata(default(ICommand), OnSelectedPageTitleCommandChanged, CoerceSelectedPageTitleCommandCommand));
        public ICommand SelectedPageTitleCommand
        {
            get => (ICommand)GetValue(SelectedPageTitleCommandProperty);
            set => SetValue(SelectedPageTitleCommandProperty, value);
        }
        private static void OnSelectedPageTitleCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pagination pagination = (Pagination)d;
            PaginationContext paginationContext = pagination.DataContext as PaginationContext;
            paginationContext.SelectedPageTitleCommand = pagination.SelectedPageTitleCommand;
        }
        private static object CoerceSelectedPageTitleCommandCommand(DependencyObject d, object baseValue)
        {
            return baseValue;
        }
    }
}
