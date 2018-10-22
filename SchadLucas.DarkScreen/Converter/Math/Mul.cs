using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SchadLucas.DarkScreen.Converter.Math
{
    public class MulConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var numbers = values.Select(n =>
            {
                float.TryParse(n.ToString(), out var parsed);
                return parsed;
            }).ToList();

            if (numbers.Count < 2)
            {
                throw new ArgumentException(nameof(values));
            }

            var result = numbers[0];

            for (var i = 1; i < numbers.Count; i++)
            {
                result *= numbers[i];
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}