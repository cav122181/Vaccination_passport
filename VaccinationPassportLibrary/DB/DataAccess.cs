using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using VaccinationPassportLibrary.Models;

namespace VaccinationPassportLibrary.DB
{
    public class DataAccess
    {
        private OleDbConnection Access { get; set; }

        /// <summary>
        /// Допомагає знайти папку проєкту
        /// </summary>
        /// <returns>Повний шлях до головної теки проєкту</returns>
        /// 


        string GetProjectDirectory()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            return projectDirectory;
        }

        public bool PersonExists(string ambCard, NotifyUser notifyUser)
        {
            string sql = $"SELECT * FROM [Person] WHERE [AmbCard] = '{ambCard}'";
            List<Person> people = GetPeople(sql, notifyUser);

            if (people.Count > 0)
            {
                notifyUser("Таку особу вже зареєстровано");
                return true;

            }

            return false;
        }
        public delegate void NotifyUser(string message);


        public DataAccess()
        {

            string dbLoc = Path.Combine(Directory.GetParent(GetProjectDirectory()).FullName, "VaccinationPassportLibrary", "PassVac.accdb");
            Access = new OleDbConnection(@$"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {dbLoc}");
        }
        //тут будуть методи

        public List<Person> GetPeople(string SQL, NotifyUser notifyUser) ///
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
                    notifyUser("Така особа не зареєстрована!");
                    //MessageBox.Show("Така особа не зареєстрована");
                    return new List<Person>();

                }
                while (reader.Read())
                {
                    Person person = new Person();
                    person.ID = (int) DBNullCheck(reader ["ID"]);
                    person.FullName = (string) DBNullCheck(reader ["FullName"]);
                    person.BirthDate = (DateTime) DBNullCheck(reader ["BirthDate"]);
                    person.AmbCard = (string) DBNullCheck(reader ["AmbCard"]);
                    person.Doctor = (string) DBNullCheck(reader ["Doctor"]);
                    person.Polyclinic = (string) DBNullCheck(reader ["Polyclinic"]);
                    person.DeclarationDate = (DateTime?) DBNullCheck(reader ["DeclarationDate"]);
                    people.Add(person);
                }

            }
            catch (OleDbException e)
            {
                string msg = "Не вдалося дістати особу";
                Debug.WriteLine(msg + e.Message);
                notifyUser(msg);
                //MessageBox.Show("Не вдалося дістати особу" +
                //    e.ToString());
                return new List<Person>();
            }
            finally
            {

                if (ToCloseOrNotToClose == true)
                    Access.Close();
            }
            return people;
        }

        public List<Vaccination> GetVaccinations(string SQL, NotifyUser notifyUser)
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
            List<Vaccination> vaccinations = new List<Vaccination>();
            try
            {
                OleDbDataReader reader = rqst.ExecuteReader();
                //перевіряємо, чи є які-небудь записи 

                if (!reader.HasRows)
                {
                    notifyUser("Така вакцинація не зареєстрована");
                    //MessageBox.Show("Така особа не зареєстрована");
                    return new List<Vaccination>();
                }
                while (reader.Read())
                {
                    int personId, vaccineId;

                    Vaccination vaccination = new Vaccination();
                    vaccination.ID = (int) DBNullCheck(reader ["Id"]);

                    vaccination.NurseFullName = (string) DBNullCheck(reader ["NurseFullName"]);

                    vaccination.VaccinationNumber = (int) DBNullCheck(reader ["VaccinationNumber"]);

                    vaccination.VaccinationDate = (DateTime) DBNullCheck(reader ["VaccinationDate"]);

                    vaccination.ExpirationDate = (DateTime?) DBNullCheck(reader ["ExpirationDate"]);

                    vaccination.SerialNumber = (string) DBNullCheck(reader ["SerialNumber"]);

                    vaccination.SideEffects = (string) DBNullCheck(reader ["SideEffects"]);

                    personId = (int) DBNullCheck(reader ["PersonId"]);
                    vaccineId = (int) DBNullCheck(reader ["VaccineId"]);

                    //vaccination.Person = (DateTime?) DBNullCheck(reader ["vaccination_date"]);
                    //vaccination.Vaccine = (string) DBNullCheck(reader ["vaccine_name"]);

                    vaccination.Person = GetPeople($"SELECT * FROM [Person] WHERE [ID]={personId}", notifyUser) [0];
                    vaccination.Vaccine = GetVaccines($"SELECT * FROM [Vaccine] WHERE [ID]={vaccineId}", notifyUser) [0];


                    vaccinations.Add(vaccination);
                }

            }
            catch (OleDbException e)
            {
                string msg = "Не вдалося дістати дані про щеплення";

                notifyUser(msg);

                Debug.WriteLine(msg + " " + e.Message);

                return new List<Vaccination>();
            }
            finally
            {
                if (ToCloseOrNotToClose == true)
                    Access.Close();
            }
            return vaccinations;
        }

        public List<Vaccine> GetVaccines(string SQL, NotifyUser notifyUser)
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
            List<Vaccine> vaccines = new List<Vaccine>();

            try
            {
                OleDbDataReader reader = rqst.ExecuteReader();

                //перевіряємо, чи є які-небудь записи 
                if (!reader.HasRows)
                {
                    string msg = "Таку вакцину не знайдено";
                    notifyUser(msg);
                    //MessageBox.Show("Таке захворювання не зареєстроване");
                    Debug.WriteLine($"{msg} {reader}");
                    return new List<Vaccine>();

                }
                while (reader.Read())
                {
                    Vaccine vaccine = new Vaccine();
                    vaccine.ID = (int) DBNullCheck(reader ["ID"]);
                    vaccine.VaccineName = (string) DBNullCheck(reader ["VaccineName"]);
                    vaccine.Dose = (double) DBNullCheck(reader ["Dose"]);

                    int diseaseId = (int) DBNullCheck(reader ["DiseaseId"]);

                    string sql = $"SELECT * FROM [Disease] WHERE [ID] = {diseaseId}";
                    vaccine.Disease = GetDiseases(sql, notifyUser) [0];

                    vaccines.Add(vaccine);
                }

            }
            catch (OleDbException e)
            {
                string msg = "Не вдалося дістати дані про щеплення";
                notifyUser(msg);
                Debug.WriteLine(msg);
                return new List<Vaccine>();
            }
            finally
            {
                if (ToCloseOrNotToClose == true)
                    Access.Close();
            }
            return vaccines;
        }

        public List<Disease> GetDiseases(string SQL, NotifyUser notifyUser)
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
                    string msg = "Таке захворювання не зареєстроване";
                    notifyUser(msg);
                    //MessageBox.Show("Таке захворювання не зареєстроване");
                    Debug.WriteLine($"{msg} {reader}");
                    return new List<Disease>();

                }
                while (reader.Read())
                {
                    Disease disease = new Disease();
                    disease.ID = (int) DBNullCheck(reader ["id"]);
                    disease.DiseaseName = (string) DBNullCheck(reader ["DiseaseName"]);
                    disease.MaxVaccinationNumber = (int) DBNullCheck(reader ["MaxVaccinationNumber"]);
                    disease.Mandatory = (bool) DBNullCheck(reader ["Mandatory"]);

                    diseases.Add(disease);
                }

            }
            catch (OleDbException e)
            {
                string msg = "Не вдалося дістати дані про щеплення";
                notifyUser(msg);
                Debug.WriteLine(msg);
                return new List<Disease>();
            }
            finally
            {
                if (ToCloseOrNotToClose == true)
                    Access.Close();
            }
            return diseases;
        }

        public bool Insert(string SQL, NotifyUser notifyUser)
        {
            Access.Open();
            OleDbCommand cmd = new OleDbCommand(SQL, Access);
            string msg;
            try
            {

                cmd.ExecuteNonQuery();


            }
            catch (OleDbException e)
            {
                msg = "Помилка запису";
                notifyUser(msg);
                Debug.WriteLine(msg + e.Message);
                //MessageBox.Show("Помилка запису" +
                //    e.ToString());

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
