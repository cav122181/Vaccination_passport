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
    /// Interaction logic for PassportWindow.xaml
    /// </summary>
    public partial class PassportWindow : Window
    {
        public PassportWindow()
        {
            InitializeComponent();
        }

        public PassportWindow(Person currentPerson)
        {
            InitializeComponent();
            PersonData.FullNameBox.Text = currentPerson.FullName;
            PersonData.BirthDateBox.Text = Convert.ToString(currentPerson.BirthDate);
            PersonData.AmbCardBox.Text = currentPerson.AmbCard;
            PersonData.DoctorBox.Text = currentPerson.Doctor;
            PersonData.ClinicBox.Text = currentPerson.Polyclinic;
            PersonData.DeclarationDateBox.Text = Convert.ToString(currentPerson.DeclDate);


        }
    }
}
