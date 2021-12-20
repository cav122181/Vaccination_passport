using System.ComponentModel;

namespace VaccinationPassportLibrary.Models
{
    public class Person : INotifyPropertyChanged
    {
        private int id;
        private string fullName;
        private DateTime? birthDate;
        private string ambCard;
        private string doctor;
        private string polyclinic;
        private DateTime? declarationDate;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string FullName
        {
            get => fullName;
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }
        public DateTime? BirthDate
        {
            get => birthDate;
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }
        public string AmbCard
        {
            get => ambCard;
            set
            {
                if (ambCard != value)
                {
                    ambCard = value;
                    OnPropertyChanged("AmbCard");
                }
            }
        }
        public string Doctor
        {
            get => doctor;
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }
        public string Polyclinic
        {
            get => polyclinic;
            set
            {
                if (polyclinic != value)
                {
                    polyclinic = value;
                    OnPropertyChanged("Polyclinic");
                }
            }
        }
        public DateTime? DeclarationDate
        {
            get => declarationDate;
            set
            {
                if (declarationDate != value)
                {
                    declarationDate = value;
                   
                    OnPropertyChanged("DeclarationDate");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString() => $"ID = {ID}, Full Name = {FullName}";

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

    }
}
