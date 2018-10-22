using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SchadLucas.DarkScreen.Converter.Math
{
    public class AddConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values
                   .Select(n =>
                   {
                       float.TryParse(n.ToString(), out var parsed);
                       return parsed;
                   })
                   .Sum();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}