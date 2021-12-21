using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VaccinationPassportLibrary.DB;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportUI.Windows
{
    /// <summary>
    /// Interaction logic for NewPassWindow.xaml
    /// </summary>
    /// 
    public partial class NewPassWindow : Window
    {
        private DataAccess dataAccess;
        public event EventHandler<EventArgs> CreateNewPassport;

        public bool CheckPersonDataFilled()
        {
            Person person = (Person) this.DataContext;

            var properties = from property in person.GetType().GetProperties()
                             select property.GetValue(person);

            //перевіримо, чи є пусті значення властивостей Person
            bool hasEmptyValues = properties.Any((x) => (string.IsNullOrWhiteSpace(Convert.ToString(x))));

            return !hasEmptyValues;

            //List<string> texts = new List<string> {
            //PersonData.FullNameBox.Text,
            //PersonData.BirthDateBox.Text,
            //PersonData.AmbCardBox.Text,
            //PersonData.DoctorBox.Text,
            //PersonData.ClinicBox.Text,
            //PersonData.DeclarationDateBox.Text
            //};

            //if (texts.Contains(""))
            //    return false;

            //return true;

        }

        public bool checkDiseaseFilled(Disease disease)
        {
            var propertiesValues = from property in disease.GetType().GetProperties()
                                   select property.GetValue(disease);

            bool hasEmptyValues = propertiesValues.Any((x) => (string.IsNullOrWhiteSpace(Convert.ToString(x))));
            return !hasEmptyValues;
        }

        //public bool checkPropertiesFilled<T>(T model)

        //{
        //    var propertiesValues = from property in model.GetType().GetProperties()
        //                           select property.GetValue(model);

        //    foreach (var propertyValue in propertiesValues)
        //    {
        //        if (propertyValue is Vaccine)
        //        {

        //        }
        //    }
        //}




        public bool CheckVaccineFilled(Vaccine vaccine)
        {
            var propertiesValues = from property in vaccine.GetType().GetProperties()
                                   where property.PropertyType != typeof(Person)
                                   select property.GetValue(vaccine);
            foreach (var propertyValue in propertiesValues)
            {
                if (propertyValue is Disease)
                {
                    if (!checkDiseaseFilled((Disease) propertyValue))
                    {
                        return false;
                    }
                }

                else
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(propertyValue)))
                    {
                        return false;



                    }
                }
            }
            //якщо все пройшло успішно, підтвердити перевірку true;
            return true;
        }

        public bool CheckVaccinationsFilled()
        {
            List<Vaccination> firstVaccinations = (List<Vaccination>) this.Resources ["FirstVaccinations"];

            foreach (var vaccination in firstVaccinations)
            {
                var propertiesValues = from property in vaccination.GetType().GetProperties()
                                       where property.PropertyType != typeof(Person)
                                       select property.GetValue(vaccination);

                //перевіримо, чи є пусті значення властивостей Vaccination

                foreach (var propertyValue in propertiesValues)
                {
                    if (propertyValue is Vaccine)
                    {
                        if (!CheckVaccineFilled((Vaccine) propertyValue))
                        {
                            return false;
                        }

                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(Convert.ToString(propertyValue)))
                        {
                            return false;

                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Потребує допрацювання
        /// </summary>
        /// 

        public NewPassWindow()
        {
            InitializeComponent();

        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            this.Close();
            //mainWindow.Show();
        }

        bool CreateVaccinations(Person person)
        {
            //DataAccess dataAccess = (DataAccess) App.Current.FindResource("SuperDB");
            List<Vaccination> vaccinations = (List<Vaccination>) this.Resources ["FirstVaccinations"];
            string sql;
            bool res = true;

            //винести в окремий метод
            foreach (var vac in vaccinations)
            {

                int vacId = vac.Vaccine.Id;
                int personId = person.ID;
                string vacDate = vac.VaccinationDate.ToString();
                string nurseName = vac.NurseFullName;
                int vaccinationNumber = vac.VaccinationNumber;
                string expDate = vac.ExpirationDate.ToString();
                string serialNumber = vac.SerialNumber;
                string sideEffects = vac.SideEffects;

                sql = $"INSERT INTO [Vaccination] " +
                $"([VaccineId], [PersonId], [VaccinationDate], [NurseFullName], " +
                $"[VaccinationNumber], [ExpirationDate], [SerialNumber], [SideEffects]) " +
                $"VALUES ({vacId}, {personId},'{vacDate}','{nurseName}',{vaccinationNumber}, '{expDate}', " +
                $"'{serialNumber}','{sideEffects}');";

                res = dataAccess.Insert(sql, DisplayMsgBox);
                if (!res)
                {
                    DisplayMsgBox("Щось пішло супернетак");
                    res = false;
                    break;
                }

            }
            return res;
        }

        Person? CreateNewPerson(Person newPerson)
        {
            //DataAccess dataAccess = (DataAccess) App.Current.FindResource("SuperDB");


            string fullName = newPerson.FullName;
            string? birthDate = newPerson.BirthDate.ToString();
            string ambCard = newPerson.AmbCard;
            string doctor = newPerson.Doctor;
            string polyclinic = newPerson.Polyclinic;
            string? declarationDate = newPerson.DeclarationDate.ToString();

            if (dataAccess.PersonExists(ambCard, DisplayMsgBox))
            {
                return null;
            }
            string sql;

            sql = $"INSERT INTO [Person] ([FullName], [BirthDate], [AmbCard], " +
            $"[Doctor], [Polyclinic], [DeclarationDate])" +
            $" VALUES ('{fullName}', '{birthDate}', '{ambCard}'," +
            $" '{doctor}', '{polyclinic}', '{declarationDate}');";

            //створюємо особу
            bool res = dataAccess.Insert(sql, DisplayMsgBox);

            //перевірка на помилки
            if (!res)
                return null;

            DisplayMsgBox("Успішно створено запис");

            //дістанемо особу за амбулаторною карткою
            sql = $"SELECT * FROM [Person] WHERE [AmbCard] = '{ambCard}'";

            Person? person = dataAccess.GetPeople(sql, DisplayMsgBox)? [0];
            return person;
        }

        private void CreateNewPassportButton_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson;
            bool personDataFilled, vaccinationDataFilled;

            newPerson = (Person) this.Resources ["newPerson"];

            personDataFilled = CheckPersonDataFilled();
            vaccinationDataFilled = CheckVaccinationsFilled();

            if (!personDataFilled || !vaccinationDataFilled)
            {
                DisplayMsgBox("Спершу заповність усі поля");
                return;
            }
            else
            {

                Person? person = CreateNewPerson(newPerson);
                DisplayMsgBox("Особу успішно зареєстровано");


                if (person != null)
                {
                    bool vaccinationsCreated = CreateVaccinations(person);
                    if (vaccinationsCreated)
                    {
                        DisplayMsgBox("Вакцинації створено");
                        this.Close();
                        PassportWindow passportWindow = new PassportWindow(person);
                    }
                    else
                        DisplayMsgBox("Помилка створення вакцнацій");
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Person person = new Person();
            this.dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");

            this.Resources ["newPerson"] = person;

            string sql;

            //DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");
            List<Vaccination> FirstVaccinations = new();

            //продумати кращий варіянт запиту
            sql = $"SELECT * FROM [Vaccine] WHERE [VaccineName] = 'ГепаВак'";
            // !!!!!!!!!!!!!!!додати перевірку 
            Vaccine hepatitisVaccine = dataAccess.GetVaccines(sql, DisplayMsgBox) [0];
            Vaccination hepatitisVaccination = new Vaccination { Person = person, VaccinationNumber = 1, Vaccine = hepatitisVaccine };


            sql = $"SELECT * FROM [Vaccine] WHERE [VaccineName] = 'БЦЖ'";
            Vaccine tuberculosisVaccine = dataAccess.GetVaccines(sql, DisplayMsgBox) [0];
            Vaccination tuberculosisVaccination = new Vaccination { Person = person, VaccinationNumber = 1, Vaccine = tuberculosisVaccine };

            FirstVaccinations.Add(hepatitisVaccination);
            FirstVaccinations.Add(tuberculosisVaccination);

            this.Resources ["FirstVaccinations"] = FirstVaccinations;
        }
    }
}
