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
            DataContext = new PaginationContext();
        }

        public static readonly DependencyProperty TotalPageCountProperty = DependencyProperty.Register(
        nameof(TotalPageCount),
        typeof(int),
        typeof(Pagination),
        new PropertyMetadata(default(int), OnTotalPageCountChanged, CoerceTotalPageCount));
        public int TotalPageCount
        {
            get => (int)GetValue(TotalPageCountProperty);
            set => SetValue(TotalPageCountProperty, value);
        }
        private static void OnTotalPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pagination pagination = (Pagination)d;
            PaginationContext paginationContext = pagination.DataContext as PaginationContext;
            paginationContext.TotalPageCount = pagination.TotalPageCount;

        }
        private static object CoerceTotalPageCount(DependencyObject d, object baseValue)
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
            PaginationContext paginationContext = pagination.DataContext as PaginationContext;
            paginationContext.CountPerPage = pagination.CountPerPage;
        }
        private static object CoerceCountPerPage(DependencyObject d, object baseValue)
        {
            return baseValue;
        }
    }
}
