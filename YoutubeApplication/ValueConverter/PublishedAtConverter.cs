using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YoutubeApplication.ValueConverter
{
    internal class PublishedAtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString())) return string.Empty;

            string valueStr = value.ToString();

            if (!DateTime.TryParse(valueStr, out DateTime uploadTime))
            {
                return valueStr;
            }

            DateTime now = DateTime.UtcNow;

            if (uploadTime > now)
            {
                return "0s";
            }

            TimeSpan timeSpan = now - uploadTime;

            // 0s ~ 59s
            if (timeSpan.TotalSeconds < 60)
                return $"{(int)timeSpan.TotalSeconds}s ago";

            // 1m ~ 59m (分鐘)
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} min ago";

            // 1h ~ 23h
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours}h ago";

            // 1d ~ 6d
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays}d ago";

            // 計算月份差異
            int monthsDifference = ((now.Year - uploadTime.Year) * 12) + now.Month - uploadTime.Month;

            // 若當前時間的「日」小於上傳時間的「日」，代表未滿整個月，需減 1
            if (now < uploadTime.AddMonths(monthsDifference))
            {
                monthsDifference--;
            }

            // 1w ~ 4w
            if (monthsDifference < 1)
            {
                int weeks = (int)(timeSpan.TotalDays / 7);
                return $"{weeks}w ago";
            }

            // 1m ~ 11m (月份)
            if (monthsDifference < 12)
                return $"{monthsDifference}mo ago";

            // 1y ~ 以上
            int yearsDifference = monthsDifference / 12;
            return $"{yearsDifference}y ago";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
