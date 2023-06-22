using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ArxLibertatisModManager.ValueConverters
{
    public class NegatingValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                return !val;
            }
            throw new ArgumentException("only bools supported");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
