using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ArxLibertatisModManager.ValueConverters
{
    public class NotEqualsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var retval = value?.Equals(parameter);
            return !retval;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
