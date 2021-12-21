using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VaccinationPassportLibrary.DB;
using VaccinationPassportLibrary.Models;
using Xceed.Wpf.Toolkit;

namespace VaccinationPassportUI.Styles
{
    public partial class ComboBoxes : ResourceDictionary
    {
        private DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");

        private void DiseaseSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            WatermarkComboBox comboBox = (WatermarkComboBox) sender;



            int? vaccineID = comboBox.SelectedValue as int?;
            if (vaccineID == null)
                return;

            string sql = $"SELECT * FROM [Vaccine] WHERE [ID] = {vaccineID}";

            List<Vaccine> tempVaccines = dataAccess.GetVaccines(sql, DisplayMsgBox);
            Vaccination vac = ((Vaccination) comboBox.DataContext);


            var motherGrid = (Grid) comboBox.Parent;
            if (motherGrid != null)
            {
                motherGrid.Resources ["VaccinesForSelectedDisease"] = tempVaccines;
                //vac.TemporaryVaccines = tempVaccines;


                //var vacBox = ((Grid) comboBox.Parent).FindName("VacName") as WatermarkComboBox;


                //vacBox.Resources ["VaccinesForSelectedDisease"] = vaccines;

            }

        }

        private void WatermarkComboBox_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            WatermarkComboBox thisBox = (WatermarkComboBox) sender;
            thisBox.SelectedIndex = 0;
        }

        private void WatermarkComboBox_Selected(object sender, RoutedEventArgs e)
        {
            var context = ((WatermarkComboBox) sender).DataContext as Vaccination;

            DisplayMsgBox(context.Vaccine.ToString());
        }

        private void WatermarkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                DisplayMsgBox(e?.AddedItems [0]?.ToString());
            var context = ((WatermarkComboBox) sender).DataContext as Vaccination;
            return;
        }
    }
}
