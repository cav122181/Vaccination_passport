using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using VaccinationPassportLibrary.Models;

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
    public class SelectedVaccineItemToIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Vaccine)
                return ((Vaccine) value).ID;
            else
                return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class VaccineToIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Vaccine)
            {
                return ((Vaccine) value).ID;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                int realValue = (int) value;
                List<Vaccine> vaccines = (List<Vaccine>)App.Current.TryFindResource("AllVaccines");
                Vaccine? theVaccine = vaccines.Find(x => x.ID == realValue);
                return theVaccine;
            }
            else
            {
                return null;
            }
        }
    }

    public class DiseaseToIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Disease)
            {
                return ((Disease) value).ID;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                int realValue = (int) value;
                List<Disease> disease = (List<Disease>) App.Current.TryFindResource("AllDiseases");
                Disease? theDisease = disease.Find(x => x.ID == realValue);
                return theDisease;
            }
            else
            {
                return null;
            }
        }
    }


}
