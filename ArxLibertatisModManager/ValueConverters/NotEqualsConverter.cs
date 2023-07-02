using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ArxLibertatisModManager.ValueConverters
{
    public class NotEqualsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool retval;
            if (value == parameter)
            {
                retval = true;
            }
            else if (value == null)
            {
                retval = false;
            }
            else
            {
                retval = value.Equals(parameter);
            }
            return !retval; //note value negation at end here
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
