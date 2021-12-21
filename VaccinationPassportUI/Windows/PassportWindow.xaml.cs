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
        private DataAccess? dataAccess;
        public PassportWindow()
        {
            InitializeComponent();
        }

        public PassportWindow(Person currentPerson)
        {
            InitializeComponent();
            this.Resources ["currentPerson"] = currentPerson;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataAccess = (DataAccess) App.Current.Resources ["SuperDB"];

            GetPersonsVaccinations();
            GetAllDiseases();
        }



        private void GetAllDiseases()
        {
            string sql = $"SELECT * FROM [Disease]";
            List<Disease> diseases = dataAccess.GetDiseases(sql, DisplayMsgBox);
            this.Resources ["AllDiseases"] = diseases;

        }
        private void GetPersonsVaccinations()
        {
            Person person = (Person) this.Resources ["currentPerson"];


            string sql = $"SELECT * FROM [Vaccination] WHERE [PersonId] = {person.ID}";

            List<Vaccination> allVaccinations = dataAccess.GetVaccinations(sql, DisplayMsgBox);


            List<Vaccination> planVaccinations = (from vacc in allVaccinations
                                                  where vacc.Vaccine.Disease.Mandatory
                                                  select vacc).ToList();

            List<Vaccination> otherVaccinations = (from vacc in allVaccinations
                                                   where !vacc.Vaccine.Disease.Mandatory
                                                   select vacc).ToList();

            // додаємо одну пусту вакцинаю в кожний список
            // для можливости заповнення нової вакцини
            planVaccinations.Add(new Vaccination());
            otherVaccinations.Add(new Vaccination());

            this.Resources ["PlanVaccinations"] = planVaccinations;
            this.Resources ["OtherVaccinations"] = otherVaccinations;
        }
        
    }
}
