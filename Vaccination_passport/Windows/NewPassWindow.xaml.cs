using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vaccination_passport.Models;

namespace Vaccination_passport.Windows
{
    /// <summary>
    /// Interaction logic for NewPassWindow.xaml
    /// </summary>
    public partial class NewPassWindow : Window
    {
        
        public NewPassWindow()
        {
            InitializeComponent();
        }

     
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            this.Close();
            //mainWindow.Show();
        }

        private void CreateNewPassportButton_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person();
            string fullName = FullName.Text;
            DateTime birthDate = DateTime.Parse(BirthDate.Text);
            string ambCard = AmbCard.Text;
            string doctor = Doctor.Text;
            string polyclinic = Clinic.Text;
            DateTime declDate = DateTime.Parse(DeclarationDate.Text);


            DateTime vacDate = DateTime.Parse(Hepatitis.VacDate.Text);
            string nurseName = Hepatitis.Nurse.Text;
            string vacName = Hepatitis.VacName.Text;
            //double dose = Convert.ToDouble(Hepatitis.Dose.Text);
            string dose = Hepatitis.Dose.Text;
            DateTime expDate = DateTime.Parse(Hepatitis.ExpDate.Text);
            string serialNumber = Hepatitis.SerialNumber.Text;
            string sideEff = Hepatitis.SideEffects.Text;

            //записуємо особу
            DataAccess dataAccess = (DataAccess)App.Current.TryFindResource("SuperDB");
            bool status = dataAccess.Insert($"INSERT INTO [Person] ([full_name], [birth_date], [amb_card], " +
                $"[doctor], [polyclinic], [declaration_date])" +
                $" VALUES ('{fullName}', '{birthDate}', '{ambCard}'," +
                $" '{doctor}', '{polyclinic}', '{declDate}');");
            
            if (status) MessageBox.Show("Запис створено");
            else MessageBox.Show("Wa wa waaa");

            //записуємо гепатит
            status = dataAccess.Insert($"INSERT INTO [Vaccination] " +
                $"([disease_id], [patient_id], [vaccination_date], [nurse_full_name], [vaccine_name], [dose], [expiration_date], [serial_number], [side_effects])" +
                $"VALUES (1, 1,'{vacDate}','{nurseName}','{vacName}', {dose},'{expDate}','{serialNumber}','{sideEff}');");

            if (status) MessageBox.Show("Запис гепатиту створено");
            else MessageBox.Show("Wa wa wa waaa");
        }
    }
}
