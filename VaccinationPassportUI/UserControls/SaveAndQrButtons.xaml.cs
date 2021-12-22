using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using IronBarCode;
using VaccinationPassportLibrary.DB;
using VaccinationPassportLibrary.Models;
using VaccinationPassportUI.Windows;

namespace VaccinationPassportUI.User_Controls
{
    /// <summary>
    /// Interaction logic for SaveAndQrButtons.xaml
    /// </summary>
    public partial class SaveAndQrButtons : UserControl
    {
        private DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");
        public SaveAndQrButtons()
        {
            InitializeComponent();
        }

        private void SaveVaccinationsAsQr(object sender, System.Windows.RoutedEventArgs e)
        {
            PassportWindow passportWindow = (PassportWindow) Window.GetWindow((Button) sender);

            Person thisPerson = (Person) passportWindow.Resources ["currentPerson"];

            DataAccess dataAccess = (DataAccess) App.Current.TryFindResource("SuperDB");
            List<Vaccination> planVaccinations = (from vaccination in (List<Vaccination>) passportWindow.Resources ["PlanVaccinations"]
                                                  where vaccination.ID != 0
                                                  select vaccination).ToList();
            List<Vaccination> otherVaccinations = (from vaccination in (List<Vaccination>) passportWindow.Resources ["OtherVaccinations"]
                                                   where vaccination.ID != 0
                                                   select vaccination).ToList();

            string strForQr = "Планові щеплення\n";

            foreach (Vaccination vaccination in planVaccinations)
                strForQr += vaccination.ToString();

            strForQr += "\nЗагальні щеплення\n";

            foreach (Vaccination vaccination in otherVaccinations)
                strForQr += vaccination.ToString();


            var dec = Encoding.Default;
            byte [] bytes = Encoding.Default.GetBytes(strForQr);
            string outputStr = Encoding.UTF8.GetString(bytes);


            string imagesDirectory = Path.Combine(dataAccess.GetProjectDirectory(), "Images");
            string pathToLogo = Path.Combine(imagesDirectory, "PassportLogo.png");
            string pathToQr = Path.Combine(imagesDirectory, $"Паспорт вакцинації {thisPerson.FullName}.png");
            try
            {
                if (File.Exists(pathToLogo))
                {

                    GeneratedBarcode qr = QRCodeWriter.CreateQrCode(strForQr, 700);
                    //var qr = BarcodeWriter.CreateBarcode(outputStr, BarcodeEncoding.QRCode);
                    qr.SaveAsPng(pathToQr);
                    DisplayMsgBox("QR-код успішно збережено (див. папку Images)");
                    BarcodeResult Result = BarcodeReader.QuicklyReadOneBarcode(pathToQr, BarcodeEncoding.QRCode);
                    if (Result != null)
                    {
                        DisplayMsgBox(Result.Text);
                    }
                }

            }


            catch (Exception ex)
            {
                DisplayMsgBox("Неможливо згенерувати QR-код " + ex.Message);

            }


        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {

            PassportWindow passportWindow = (PassportWindow) Window.GetWindow((Button) sender);

            List<Vaccination> vaccinations;
            if (passportWindow.VaccinationsTypeTabs.SelectedIndex == 0)
                vaccinations = (List<Vaccination>) passportWindow.Resources ["PlanVaccinations"];
            else
            {
                vaccinations = (List<Vaccination>) passportWindow.Resources ["OtherVaccinations"];
            }
            //особа
            Person person = (Person) passportWindow.Resources ["currentPerson"];


            //планові вакцинації
            //List<Vaccination> allVaccinations = (List<Vaccination>) passportWindow.Resources ["PlanVaccinations"];
           
            //загальні вакцинації
            //List<Vaccination> otherVaccinations = (List<Vaccination>) passportWindow.Resources ["OtherVaccinations"];

            //allVaccinations.AddRange(otherVaccinations);

            //var vaccinationsToAdd = (from vaccination in allVaccinations
            //                         where vaccination.ID == 0
                                     //select vaccination).ToList();
            var vaccinationsToAdd = (from vaccination in vaccinations
                                     where vaccination.ID == 0
                                     select vaccination).ToList();


            bool vaccinationDataFilled = CheckVaccinationsFilled(vaccinationsToAdd);

            if (!vaccinationDataFilled)
            {
                DisplayMsgBox("Спершу заповність усі поля");
                return;
            }
            else
            {
                bool vaccinationsCreated = CreateVaccinations(person, vaccinationsToAdd);
                if (vaccinationsCreated)
                {
                    DisplayMsgBox("Вакцинації створено");
                    passportWindow.GetPersonsVaccinations();
                }
                else
                {
                    DisplayMsgBox("ЩОсь пішло не так");
                }
            }

        }

        bool CreateVaccinations(Person person, List<Vaccination> vaccinations)
        {
            //DataAccess dataAccess = (DataAccess) App.Current.FindResource("SuperDB");
            string sql;
            bool res = true;

            //винести в окремий метод
            foreach (var vac in vaccinations)
            {

                int vacId = vac.Vaccine.ID;
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


        public bool CheckVaccinationsFilled(List<Vaccination> vaccinations)
        {

            foreach (var vaccination in vaccinations)
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

        public bool checkDiseaseFilled(Disease disease)
        {
            var propertiesValues = from property in disease.GetType().GetProperties()
                                   select property.GetValue(disease);

            bool hasEmptyValues = propertiesValues.Any((x) => (string.IsNullOrWhiteSpace(Convert.ToString(x))));
            return !hasEmptyValues;
        }
    }

}
