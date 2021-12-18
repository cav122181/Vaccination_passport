using System.Collections.Generic;
using System.Windows;
using Vaccination_passport.Models;

namespace Vaccination_passport.Windows
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
            string name = SearchBox.Text;
            DataAccess dataAccess = (DataAccess)App.Current.TryFindResource("SuperDB");
            List<Person> people = dataAccess.GetPeople($"SELECT * FROM [Person] WHERE [full_name]='{name}';");


            //MessageBox.Show(person.ToString());
            PassportWindow PassWindow = new PassportWindow(people[0]);
            PassWindow.Resources["currentPerson"] = people[0];
            PassWindow.Show();


        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            PassportWindow PassWindow = new PassportWindow();
            PassWindow.Show();

        }
    }
}
