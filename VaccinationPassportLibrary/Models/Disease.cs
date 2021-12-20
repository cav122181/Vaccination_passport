namespace VaccinationPassportLibrary.Models
{
    public class Disease
    {
        public int ID { get; set; }
        public string DiseaseName { get; set; }
        public int MaxVaccinationNumber { get; set; }
        public bool Mandatory { get; set; }
    }
}
