using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportUI.User_Controls
{
    /// <summary>
    /// Interaction logic for PersonDataBlock.xaml
    /// </summary>
    public partial class PersonDataBlock : UserControl, INotifyPropertyChanged
    {
        //public int ID { get; set; }
        //public string FullName { get; set; }

        //public DateTime? BirthDate { get; set; }
        //public string AmbCard { get; set; }
        //public string Doctor { get; set; }
        //public string Polyclinic { get; set; }
        //public DateTime? DeclDate { get; set; }

        private Person selectedPerson;

        public static readonly DependencyProperty SelectedPersonProperty = DependencyProperty.Register(
            "SelectedPerson", typeof(Person), typeof(PersonDataBlock)
            );
        public Person SelectedPerson
        {
            get
            {
                return (Person) GetValue(SelectedPersonProperty);
            }
            set
            {
                SetValue(SelectedPersonProperty, value);
                //selectedPerson = value;
                //NotifyPropertyChanged("SelectedPerson");
            }
        }

        public PersonDataBlock()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
