using System;
using System.Windows;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportUI.Windows
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
            PersonData.DeclarationDateBox.Text = Convert.ToString(currentPerson.DeclarationDate);


        }
    }
}
