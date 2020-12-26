using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace CW.Converters
{
    public class OpenPeriodConverter : IValueConverter
    {
        private readonly string preffix = "Часы работы: ";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as List<string>;

            if (list == null || list.Count == 0)
            {
                return preffix + "Нет информации";
            }
            string week = DateTime.Now.ToString("dddd", new CultureInfo("ru-RU"));
             
            var tmp =  preffix + list.Where(x => x.Contains(week)).Select(x => x.Split()[x.Split().Length > 1 ? 1 : 0]).FirstOrDefault();
            return tmp;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
