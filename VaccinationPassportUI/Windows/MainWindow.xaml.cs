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
            string sql;
            string id = SearchBox.Text;
            DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");

            sql = $"SELECT * FROM [Person] WHERE [ID]={id};";
            List<Person> people = dataAccess.GetPeople(sql, DisplayMsgBox);


           
            //перевірити, коли немає користувача
            if (people.Count > 0)
            {
                PassportWindow PassWindow = new PassportWindow(people [0]);
                PassWindow.Resources ["currentPerson"] = people [0];
                PassWindow.Show();

            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            PassportWindow PassWindow = new PassportWindow();
            PassWindow.Show();

        }
    }
}
