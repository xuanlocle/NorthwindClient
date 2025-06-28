using System.Globalization;

namespace NorthwindClient.Converter;

public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool booleanValue)
            return !booleanValue; // Invert the boolean value

        return false; // Return false if it's not a boolean
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}