using System;
using System.Globalization;
using System.Windows.Data;
namespace VaccinationPassportUI.Converters
{
    public class IsVaccinationReadOnlyConverter : IValueConverter
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
            var param = parameter as string;
            if (param == "reverse")
                return !isReadonly;
            else return isReadonly;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


}
