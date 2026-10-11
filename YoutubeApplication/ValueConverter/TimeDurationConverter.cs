using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace YoutubeApplication.ValueConverter
{
    internal class TimeDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // PT1H56M4S: 1:56:04
            // PT24S: 0:24
            // PT20M0S: 20:00
            // PT1H3S: 1:00:03

            string valueStr = value.ToString();

            TimeSpan ts = XmlConvert.ToTimeSpan(valueStr);

            if (ts.TotalHours >= 1)
            {
                return $"{(int)ts.TotalHours}:{ts:mm\\:ss}";
            }
            else
            {
                return $"{ts.Minutes}:{ts:ss}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
