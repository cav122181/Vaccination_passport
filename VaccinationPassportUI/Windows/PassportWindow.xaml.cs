using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VaccinationPassportLibrary.DB;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportUI.Windows
{
    /// <summary>
    /// Interaction logic for PassportWindow.xaml
    /// </summary>
    public partial class PassportWindow : Window
    {
        private DataAccess dataAccess;
        public PassportWindow()
        {
            InitializeComponent();
        }

        public PassportWindow(Person currentPerson)
        {
            InitializeComponent();
            this.Resources ["currentPerson"] = currentPerson;
            //PersonData.FullNameBox.Text = currentPerson.FullName;
            //PersonData.BirthDateBox.Text = Convert.ToString(currentPerson.BirthDate);
            //PersonData.AmbCardBox.Text = currentPerson.AmbCard;
            //PersonData.DoctorBox.Text = currentPerson.Doctor;
            //PersonData.ClinicBox.Text = currentPerson.Polyclinic;
            //PersonData.DeclarationDateBox.Text = Convert.ToString(currentPerson.DeclarationDate);


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataAccess = (DataAccess) App.Current.Resources ["SuperDB"];

            Person person = (Person) this.Resources ["currentPerson"];


            string sql = $"SELECT * FROM [Vaccination] WHERE [PersonId] = {person.ID}";

            List<Vaccination> allVaccinations = dataAccess.GetVaccinations(sql, DisplayMsgBox);


            //List<Vaccination> planVaccinations = dataAccess.GetVaccinations(sql, DisplayMsgBox);
            List<Vaccination> planVaccinations = (from vacc in allVaccinations
                                                  where vacc.Vaccine.Disease.Mandatory
                                                  select vacc).ToList();

            List<Vaccination> otherVaccinations = (from vacc in allVaccinations
                                                   where !vacc.Vaccine.Disease.Mandatory
                                                   select vacc).ToList();

            this.Resources ["PlanVaccinations"] = planVaccinations;
            this.Resources ["OtherVaccinations"] = otherVaccinations;
        }
    }
}
