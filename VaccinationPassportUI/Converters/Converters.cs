using System;
using System.Globalization;
using System.Windows.Data;
namespace VaccinationPassportUI.Converters
{
    internal class IsVaccinationReadOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isReadonly = false;
            if (value is int)
            {
                switch ((int) value)
                {
                    case 0:
                        isReadonly = false;
                        break;
                    default:
                        isReadonly = true;
                        break;
                }
            }
            return isReadonly;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
