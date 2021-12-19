namespace VaccinationPassportLibrary.Models
{
    public class Vaccination
    {
        public int ID { get; set; }

        public Vaccine Vaccine { get; set; }
        public Person? Person { get; set; }

        public string NurseFullName { get; set; }
        public int VaccinationNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string SerialNumber { get; set; }
        public string SideEffects { get; set; }
    }
}
