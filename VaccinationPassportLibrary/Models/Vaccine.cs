namespace VaccinationPassportLibrary.Models
{
    public class Vaccine
    {
        public int ID { get; set; }
        public Disease? Disease { get; set; }
        public string VaccineName { get; set; }
        public double Dose { get; set; }

        public override string ToString()
        {
            return $"ID = {ID}, Disease is {Disease.ToString()} VaccineName = {VaccineName}";
        }
    }
}
