using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using CW.Models;

namespace CW.Converters
{
    public class BankNumberConverter : IValueConverter
    {
        public string TypeName { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var info = value as string;

            if (info == null)
            {
                return value;
            }

            if (Type.GetType(TypeName) == typeof(BankCard))
            {
                return ConstructStringToReplace(info, 4, 4);
            }

            if (Type.GetType(TypeName) == typeof(BankAccount))
            {
                return ConstructStringToReplace(info, 0, 6);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string ConstructStringToReplace(string str, int countStartNumbers, int countEndNumbers)
        {
            int countNumbersToReplace = str.Length - countStartNumbers - countEndNumbers;
            String replace = new String('*', countNumbersToReplace);

            return str.Remove(countStartNumbers, countNumbersToReplace).Insert(countStartNumbers, replace.ToString());
        }
    }
}
