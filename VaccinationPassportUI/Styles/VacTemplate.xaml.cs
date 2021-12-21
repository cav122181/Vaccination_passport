using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using VaccinationPassportUI.Windows;
using Xceed.Wpf.Toolkit;
using VaccinationPassportLibrary.Models;
using VaccinationPassportLibrary.DB;

namespace VaccinationPassportUI.Styles
{
    public partial class VacTemplate:ResourceDictionary
    {
        private DataAccess dataAccess = (DataAccess)App.Current.TryFindResource("SuperDB");

        private void WatermarkComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            WatermarkComboBox comboBox = (WatermarkComboBox) sender;



            int vaccineID = (int)comboBox.SelectedValue;

            string sql = $"SELECT * FROM [Vaccine] WHERE [ID] = {vaccineID}";

            List<Vaccine> vaccines = dataAccess.GetVaccines(sql, DisplayMsgBox);

            
            var vacBox = ((Grid) comboBox.Parent).FindName("VacName") as WatermarkComboBox;
            vacBox.Resources ["VaccinesForSelectedDisease"] = vaccines;



        }
    }
}
