using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportLibrary.Database
{
    internal class DataAccess
    {
        private OleDbConnection Access { get; set; }

        /// <summary>
        /// Допомагає знайти папку проєкту
        /// </summary>
        /// <returns>Повний шлях до головної теки проєкту</returns>
        string GetProjectDirectory()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            return projectDirectory;
        }

        public DataAccess()
        {
            string dbLoc = Path.Combine(GetProjectDirectory(), "PassVac.accdb");
            Access = new OleDbConnection(@$"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {dbLoc}");
        }
        //тут будуть методи

        public List<Person> GetPeople(string SQL) ///
        {

            bool ToCloseOrNotToClose;
            if (Access.State == ConnectionState.Open)
                ToCloseOrNotToClose = false;
            else
            {
                Access.Open();
                ToCloseOrNotToClose = true;
            }

            OleDbCommand rqst = new OleDbCommand(SQL, Access);
            List<Person> people = new List<Person>();
            try
            {
                OleDbDataReader reader = rqst.ExecuteReader();
                //перевіряємо, чи є 

                if (!reader.HasRows)
                {
                    MessageBox.Show("Така особа не зареєстрована");
                    return new List<Person>();

                }
                while (reader.Read())
                {
                    Person person = new Person();
                    person.ID = (int) DBNullCheck(reader ["id"]);
                    person.FullName = (string) DBNullCheck(reader ["full_name"]);
                    person.BirthDate = (DateTime) DBNullCheck(reader ["birth_date"]);
                    person.AmbCard = (string) DBNullCheck(reader ["amb_card"]);
                    person.Doctor = (string) DBNullCheck(reader ["doctor"]);
                    person.Polyclinic = (string) DBNullCheck(reader ["polyclinic"]);
                    person.DeclDate = (DateTime?) DBNullCheck(reader ["declaration_date"]);
                    people.Add(person);
                }

            }
            catch (OleDbException e)
            {
                MessageBox.Show("Не вдалося дістати особу" +
                    e.ToString());
                return new List<Person>();
            }
            finally
            {

                if (ToCloseOrNotToClose == true)
                    Access.Close();
            }
            return people;
        }
        public List<Vaccination> GetVaccinations(string SQL)
        {


            OleDbCommand rqst = new OleDbCommand(SQL, Access);
            List<Vaccination> vaccinations = new List<Vaccination>();
            try
            {
                OleDbDataReader reader = rqst.ExecuteReader();
                //перевіряємо, чи є які-небудь записи 

                if (!reader.HasRows)
                {
                    MessageBox.Show("Така особа не зареєстрована");
                    return new List<Vaccination>();

                }
                while (reader.Read())
                {
                    int personId;
                    Vaccination vaccination = new Vaccination();
                    vaccination.ID = (int) DBNullCheck(reader ["id"]);
                    vaccination.VacDate = (DateTime?) DBNullCheck(reader ["vaccination_date"]);
                    vaccination.Nurse = (string) DBNullCheck(reader ["nurse_full_name"]);
                    vaccination.VacName = (string) DBNullCheck(reader ["vaccine_name"]);
                    vaccination.Dose = (float) DBNullCheck(reader ["dose"]);
                    vaccination.ExpirationDate = (DateTime?) DBNullCheck(reader ["expiration_date"]);
                    vaccination.SerialNumber = (string) DBNullCheck(reader ["serial_number"]);
                    vaccination.SideEff = (string) DBNullCheck(reader ["side_effects"]);
                    personId = (int) DBNullCheck(reader ["person_id"]);

                    //
                    vaccination.Person = GetPeople($"SELECT * FROM [Person] WHERE [person_id]={personId}") [0];
                    vaccination.Disease = GetDiseases($"") [0];

                    //vaccination.Person 0 
                    //vaccination.Disease 

                    vaccinations.Add(vaccination);
                }

            }
            catch (OleDbException e)
            {
                MessageBox.Show("Не вдалося дістати про щеплення" +
                    e.ToString());
                return new List<Vaccination>();
            }
            finally
            {
                Access.Close();
            }
            return vaccinations;
        }
        public List<Disease> GetDiseases(string SQL)
        {

            bool ToCloseOrNotToClose;
            if (Access.State == ConnectionState.Open)
                ToCloseOrNotToClose = false;
            else
            {
                Access.Open();
                ToCloseOrNotToClose = true;
            }

            OleDbCommand rqst = new OleDbCommand(SQL, Access);
            List<Disease> diseases = new List<Disease>();
            try
            {
                OleDbDataReader reader = rqst.ExecuteReader();
                //перевіряємо, чи є які-небудь записи 

                if (!reader.HasRows)
                {
                    MessageBox.Show("Таке захворювання не зареєстроване");
                    return new List<Disease>();

                }
                while (reader.Read())
                {
                    Disease disease = new Disease();
                    disease.ID = (int) DBNullCheck(reader ["id"]);
                    disease.DisName = (string) DBNullCheck(reader ["dis_name"]);
                    disease.VaccineNumber = (int) DBNullCheck(reader ["vaccination_number"]);

                    diseases.Add(disease);
                }

            }
            catch (OleDbException e)
            {
                MessageBox.Show("Не вдалося дістати про щеплення" +
                    e.ToString());
                return new List<Disease>();
            }
            finally
            {
                if (ToCloseOrNotToClose == true)
                    Access.Close();
            }
            return diseases;
        }

        public bool Insert(string SQL)
        {
            Access.Open();
            OleDbCommand cmd = new OleDbCommand(SQL, Access);

            try
            {

                cmd.ExecuteNonQuery();


            }
            catch (OleDbException e)
            {
                MessageBox.Show("Помилка запису" +
                    e.ToString());

                return false;
            }
            finally
            {
                Access.Close();
            }

            return true;

        }

        //метод для перетворення у нормальний null
        private object DBNullCheck(object field)
        {
            if (field == DBNull.Value)
                return null;
            else return field;
        }
    }
}
