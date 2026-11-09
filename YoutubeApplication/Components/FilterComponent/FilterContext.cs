using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeApplication.Models;
using YoutubeApplication.Utility;

namespace YoutubeApplication.Components.FilterComponent
{
    [AddINotifyPropertyChangedInterface]
    internal class FilterContext
    {
        public ICommand ConfirmFilterCommand { get; set; }
        public ICommand GetConfirmFilterCommand { get; set; }

        public FilterContext()
        {
            this.ConfirmFilterCommand = new RelayCommand<SearchFilter>(ConfirmFilterTask, (parameter) => true);
        }

        private void ConfirmFilterTask(SearchFilter parm)
        {
            SearchFilter filter = new SearchFilter();

            Type type = typeof(FilterContext);

            PropertyInfo[] propertyInfos = type.GetProperties().Where(x => x.PropertyType == typeof(ObservableCollection<OptionItem>)).ToArray();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                foreach (OptionItem item in (IList)propertyInfo.GetValue(this))
                {
                    string filterPropertyName = propertyInfo.Name.Replace("Items", "");

                    PropertyInfo filterProperty = filter.GetType().GetProperty(filterPropertyName);
                    if (item.IsChecked) filterProperty.SetValue(filter, item.Title);
                    if (filterProperty.GetValue(filter) != null) break;
                }
            }

            this.GetConfirmFilterCommand.Execute(filter);
        }

        public ObservableCollection<OptionItem> SearchTypeItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("Channel", "SearchType"),
             new OptionItem("playlist", "SearchType"),
             new OptionItem("video", "SearchType"),
        };

        public ObservableCollection<OptionItem> VideoDurationItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("short", "VideoDuration"),
             new OptionItem("long", "VideoDuration"),
        };

        public ObservableCollection<OptionItem> VideoDefinitionItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("high", "VideoDefinition"),
             new OptionItem("standard", "VideoDefinition"),
        };

        public ObservableCollection<OptionItem> VideoTypeItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("episode", "VideoType"),
             new OptionItem("movie", "VideoType")
        };

        public ObservableCollection<OptionItem> PublishedAfterItems { get; set; } = new ObservableCollection<OptionItem>()
        {
             new OptionItem("today", "PublishedAfter"),
             new OptionItem("this_week", "PublishedAfter"),
             new OptionItem("this_month", "PublishedAfter"),
        };
    }
}
