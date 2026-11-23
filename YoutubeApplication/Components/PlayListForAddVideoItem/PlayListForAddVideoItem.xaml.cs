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
using YoutubeApplication.Models;

namespace YoutubeApplication.Components.PlayListForAddVideoItem
{
    /// <summary>
    /// Interaction logic for PlayListForAddVideoItem.xaml
    /// </summary>
    public partial class PlayListForAddVideoItem : UserControl
    {
        public PlayListForAddVideoItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AddVideoItemPlayListCommandProperty = DependencyProperty.Register(
            nameof(AddVideoItemPlayListCommand),
            typeof(ICommand),
            typeof(PlayListForAddVideoItem),
            new PropertyMetadata(default(ICommand)));

        public ICommand AddVideoItemPlayListCommand
        {
            get => (ICommand)GetValue(AddVideoItemPlayListCommandProperty);
            set => SetValue(AddVideoItemPlayListCommandProperty, value);
        }

        public static readonly DependencyProperty RemoveVideoItemPlayListCommandProperty = DependencyProperty.Register(
           nameof(RemoveVideoItemPlayListCommand),
           typeof(ICommand),
           typeof(PlayListForAddVideoItem),
           new PropertyMetadata(default(ICommand)));

        public ICommand RemoveVideoItemPlayListCommand
        {
            get => (ICommand)GetValue(AddVideoItemPlayListCommandProperty);
            set => SetValue(AddVideoItemPlayListCommandProperty, value);
        }
    }
}
