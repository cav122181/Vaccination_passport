using System.Collections.Generic;
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

        /// <summary>
        /// Потребує допрацювання
        /// </summary>
        public NewPassWindow()
        {
            InitializeComponent();

            string sql;

            DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");
            List<Vaccination> FirstVaccinations = new();

            //продумати кращий варіянт запиту
            sql = $"SELECT * FROM [Vaccine] WHERE [VaccineName] = 'ГепаВак'";
            // !!!!!!!!!!!!!!!додати перевірку 
            Vaccine hepatitisVaccine = dataAccess.GetVaccines(sql, DisplayMsgBox) [0];
            Vaccination hepatitisVaccination = new Vaccination { VaccinationNumber = 1, Vaccine = hepatitisVaccine };


            sql = $"SELECT * FROM [Vaccine] WHERE [VaccineName] = 'БЦЖ'";
            Vaccine tuberculosisVaccine = dataAccess.GetVaccines(sql, DisplayMsgBox) [0];
            Vaccination tuberculosisVaccination = new Vaccination { VaccinationNumber = 1, Vaccine = tuberculosisVaccine };

            FirstVaccinations.Add(hepatitisVaccination);
            FirstVaccinations.Add(tuberculosisVaccination);

            this.Resources ["FirstVaccinations"] = FirstVaccinations;
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
            DataAccess dataAccess = (DataAccess)App.Current.FindResource("SuperDB");

            string fullName = PersonData.FullNameBox.Text;
            string birthDate = PersonData.BirthDateBox.Text;
            string ambCard = PersonData.AmbCardBox.Text;
            string doctor = PersonData.DoctorBox.Text;
            string polyclinic = PersonData.ClinicBox.Text;
            string declarationDate = PersonData.DeclarationDateBox.Text;
 

            sql = $"INSERT INTO [Person] ([FullName], [BirthDate], [AmbCard], " +
            $"[Doctor], [Polyclinic], [DeclarationDate])" +
            $" VALUES ('{fullName}', '{birthDate}', '{ambCard}'," +
            $" '{doctor}', '{polyclinic}', '{declarationDate}');";

            //створюємо особу
            bool res = dataAccess.Insert(sql, DisplayMsgBox);

            //перевірка на помилки
            if (!res)
                return;

            //дістанемо особу за амбулаторною карткою
            sql = $"SELECT * FROM [Person] WHERE [AmbCard] = '{ambCard}'";

            Person? person = dataAccess.GetPeople(sql, DisplayMsgBox)?[0];

            //винести в окремий метод
            List<Vaccination> vaccinations = (List<Vaccination>) this.Resources ["FirstVaccinations"];
            foreach (var vac in vaccinations)
            {

                int vacId = vac.Vaccine.Id;
                int personId = person.ID;
                string vacDate = vac.VaccinationDate.ToString();
                string nurseName = vac.NurseFullName;
                int vaccinationNumber = vac.VaccinationNumber;
                string expDate = vac.ExpirationDate.ToString();
                string serialNumber = vac.SerialNumber;
                string sideEffects = vac.SideEffects;

                sql = $"INSERT INTO [Vaccination] " +
                $"([VaccineId], [PersonId], [VaccinationDate], [NurseFullName], " +
                $"[VaccinationNumber], [ExpirationDate], [SerialNumber], [SideEffects]) " +
                $"VALUES ({vacId}, {personId},'{vacDate}','{nurseName}',{vaccinationNumber}, '{expDate}', " +
                $"'{serialNumber}','{sideEffects}');";

                res = dataAccess.Insert(sql, DisplayMsgBox);
                if (!res)
                {
                    DisplayMsgBox("Щось пішло супернетак");
                    break;
                }    

            }

            /*
            Person person = new Person();
            string fullName = FullName.Text;
            DateTime birthDate = DateTime.Parse(BirthDate.Text);
            string ambCard = AmbCard.Text;
            string doctor = Doctor.Text;
            string polyclinic = Clinic.Text;
            DateTime declarationDate = DateTime.Parse(DeclarationDate.Text);


            DateTime vacDate = DateTime.Parse(Hepatitis.VacDate.Text);
            string nurseName = Hepatitis.Nurse.Text;
            string vacName = Hepatitis.VacName.Text;
            //double dose = Convert.ToDouble(Hepatitis.Dose.Text);
            string dose = Hepatitis.Dose.Text;
            DateTime expDate = DateTime.Parse(Hepatitis.ExpDate.Text);
            string serialNumber = Hepatitis.SerialNumber.Text;
            string sideEff = Hepatitis.SideEffects.Text;

            записуємо особу
            DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");

            sql = $"INSERT INTO [Person] ([FullName], [BirthDate], [AmbCard], " +
                $"[Doctor], [Polyclinic], [DeclarationDate])" +
                $" VALUES ('{fullName}', '{birthDate}', '{ambCard}'," +
                $" '{doctor}', '{polyclinic}', '{declarationDate}');";
            */

            //bool status = dataAccess.Insert(sql, DisplayMsgBox);

            ////TODO: прибрати це повідомлення до клясу DataAccess
            //if (status) MessageBox.Show("Запис створено");
            //else MessageBox.Show("Wa wa waaa");

            ////записуємо гепатит

            //sql = $"INSERT INTO [Vaccination] " +
            //    $"([disease_id], [patient_id], [vaccination_date], [nurse_full_name], [vaccine_name], [dose], [expiration_date], [serial_number], [side_effects])" +
            //    $"VALUES (1, 1,'{vacDate}','{nurseName}','{vacName}', {dose},'{expDate}','{serialNumber}','{sideEff}');";
            //status = dataAccess.Insert(sql, DisplayMsgBox);

            //if (status) MessageBox.Show("Запис гепатиту створено");
            //else MessageBox.Show("Wa wa wa waaa");
        }
    }
}
