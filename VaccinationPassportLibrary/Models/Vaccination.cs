using System.ComponentModel;

namespace VaccinationPassportLibrary.Models
{
    public class Vaccination : INotifyPropertyChanged
    {
        private int iD;
        private Vaccine? vaccine;
        private Person? person;
        private DateTime? vaccinationDate;
        private string? nurseFullName;
        private int vaccinationNumber;
        private DateTime? expirationDate;
        private string? serialNumber;
        private string? sideEffects;





        public int ID
        {
            get => iD; set
            {
                iD = value;
                OnPropertyChanged(nameof(ID));
            }
        }
        public Vaccine Vaccine
        {
            get => vaccine; set
            {
                vaccine = value;
                OnPropertyChanged(nameof(Vaccine));
            }
        }
        public Person? Person
        {
            get => person; set
            {
                person = value;
                OnPropertyChanged(nameof(Person));
            }
        }
        public DateTime? VaccinationDate
        {
            get => vaccinationDate; set
            {
                vaccinationDate = value;
                OnPropertyChanged(nameof(VaccinationDate));
            }
        }
        public string? NurseFullName
        {
            get => nurseFullName; set
            {
                nurseFullName = value;
                OnPropertyChanged(nameof(NurseFullName));
            }
        }
        public int VaccinationNumber
        {
            get => vaccinationNumber; set
            {
                vaccinationNumber = value;
                OnPropertyChanged(nameof(VaccinationNumber));
            }
        }
        public DateTime? ExpirationDate
        {
            get => expirationDate; set
            {
                expirationDate = value;
                OnPropertyChanged(nameof(ExpirationDate));
            }
        }
        public string? SerialNumber
        {
            get => serialNumber; set
            {
                serialNumber = value;
                OnPropertyChanged(nameof(SerialNumber));
            }

        }
        public string? SideEffects
        {
            get => sideEffects; set
            {
                sideEffects = value;
                OnPropertyChanged(nameof(SideEffects));
            }
        }

        public List<Vaccine> TemporaryVaccines { get; set; } = new List<Vaccine>();

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            string format =

            $"\nПІБ особи: {Person.FullName}\n" +
            $"Дата народження: {Person.BirthDate}\n" +
            $"Проти хвороби: {Vaccine.Disease.DiseaseName}\n" +
            $"Щеплення №: {ID}\n" +
            $"Назва вакцини: {Vaccine.VaccineName}\n" +
            $"Дата щеплення {VaccinationDate}\n" +
            $"Побічні реякції {SideEffects}\n";

            return format;

        }
    }
}
