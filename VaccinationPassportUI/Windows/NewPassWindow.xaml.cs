using System;
using System.Windows;
using VaccinationPassportLibrary.DB;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportUI.Windows
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

            string sql;

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
            DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");

            sql = $"INSERT INTO [Person] ([full_name], [birth_date], [amb_card], " +
                $"[doctor], [polyclinic], [declaration_date])" +
                $" VALUES ('{fullName}', '{birthDate}', '{ambCard}'," +
                $" '{doctor}', '{polyclinic}', '{declDate}');";

            bool status = dataAccess.Insert(sql, DisplayMsgBox);

            if (status) MessageBox.Show("Запис створено");
            else MessageBox.Show("Wa wa waaa");

            //записуємо гепатит
            sql = $"INSERT INTO [Vaccination] " +
                $"([disease_id], [patient_id], [vaccination_date], [nurse_full_name], [vaccine_name], [dose], [expiration_date], [serial_number], [side_effects])" +
                $"VALUES (1, 1,'{vacDate}','{nurseName}','{vacName}', {dose},'{expDate}','{serialNumber}','{sideEff}');";
            status = dataAccess.Insert(sql, DisplayMsgBox);

            if (status) MessageBox.Show("Запис гепатиту створено");
            else MessageBox.Show("Wa wa wa waaa");
        }
    }
}
