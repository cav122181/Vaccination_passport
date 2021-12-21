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
            byte [] bytes =Encoding.Default.GetBytes(strForQr);
            string outputStr = Encoding.UTF8.GetString(bytes);


            string imagesDirectory = Path.Combine(dataAccess.GetProjectDirectory(), "Images");
            string pathToLogo = Path.Combine(imagesDirectory, "PassportLogo.png");
            string pathToQr = Path.Combine(imagesDirectory, $"Паспорт вакцинації {thisPerson.FullName}.png");
            try
            {
                if (File.Exists(pathToLogo))
                {
                   
                    //GeneratedBarcode qr = QRCodeWriter.CreateQrCode(strForQr, 300);
                    var qr = BarcodeWriter.CreateBarcode(outputStr,BarcodeEncoding.QRCode);
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
    }
}
