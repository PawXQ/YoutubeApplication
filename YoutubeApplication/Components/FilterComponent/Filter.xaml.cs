using System;
using System.Collections.Generic;
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

namespace YoutubeApplication.Components.FilterComponent
{
    /// <summary>
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : UserControl
    {
        public Filter()
        {
            InitializeComponent();
            DataContext = new FilterContext();
        }

        public static readonly DependencyProperty GetConfirmFilterCommandProperty = DependencyProperty.Register(
        nameof(GetConfirmFilterCommand),
        typeof(ICommand),
        typeof(Filter),
        new PropertyMetadata(default(ICommand), GetConfirmFilterCommandChanged, CoerceGetConfirmFilterCommand));
        public ICommand GetConfirmFilterCommand
        {
            get => (ICommand)GetValue(GetConfirmFilterCommandProperty);
            set => SetValue(GetConfirmFilterCommandProperty, value);
        }
        private static void GetConfirmFilterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Filter panel = d as Filter;
            FilterContext filterContext = panel.DataContext as FilterContext;
            filterContext.GetConfirmFilterCommand = e.NewValue as ICommand;
        }
        private static object CoerceGetConfirmFilterCommand(DependencyObject d, object baseValue)
        {
            return baseValue;
        }
    }
}
