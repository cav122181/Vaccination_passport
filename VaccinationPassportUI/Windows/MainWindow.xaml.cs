using System.Collections.Generic;
using System.Windows;
using VaccinationPassportLibrary.DB;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportUI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataAccess? dataAccess;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void new_passport_Click(object sender, RoutedEventArgs e)
        {
            NewPassWindow newPassWindow = new NewPassWindow();
            newPassWindow.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {


            if (PersonSearchBox.SelectedValue == null)
            {
                DisplayMsgBox("Будь ласка, оберіть особу зі списку.");
                return;
            }
            //string sql;

            //string id = PersonSearchBox.Text;


            //DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");

            //sql = $"SELECT * FROM [Person] WHERE [ID]={id};";
            //List<Person> people = dataAccess.GetPeople(sql, DisplayMsgBox);



            //перевірити, коли немає користувача
            //if (people.Count > 0)
            //{
            //    PassportWindow PassWindow = new PassportWindow(people [0]);
            //    PassWindow.Resources ["currentPerson"] = people [0];
            //    PassWindow.Show();

            //}

            Person person = (Person) PersonSearchBox.SelectedItem;

            PassportWindow PassWindow = new PassportWindow(person);
            //PassWindow.Resources ["currentPerson"] = person;
            PassWindow.Show();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            PassportWindow PassWindow = new PassportWindow();
            PassWindow.Show();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataAccess = (DataAccess) App.Current.FindResource("SuperDB");

            GetAllPeople();
            GetAllDiseases();
            GetAllVaccines();

        }

        /// <summary>
        /// Установлює список захворювань для всієї програми
        /// </summary>
        private void GetAllDiseases()
        {
            string sql = $"SELECT * FROM [Disease]";
            List<Disease> diseases = dataAccess.GetDiseases(sql, DisplayMsgBox);


            //
            App.Current.Resources ["AllDiseases"] = diseases;

        }

        private void GetAllVaccines()
        {
            string sql = $"SELECT * FROM [Vaccine]";
            List<Vaccine> vaccines = dataAccess.GetVaccines(sql, DisplayMsgBox);


            //
            App.Current.Resources ["AllVaccines"] = vaccines;

        }


        private void GetAllPeople()
        {
            string sql;
            sql = "SELECT * FROM [Person]";

            List<Person> people = dataAccess.GetPeople(sql, DisplayMsgBox);

            App.Current.Resources ["AllPeople"] = people;
        }
    }
}
