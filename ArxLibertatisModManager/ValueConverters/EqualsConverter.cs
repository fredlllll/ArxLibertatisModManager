using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.ValueConverters
{
    public class EqualsConverter : IValueConverter
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
            return retval;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
