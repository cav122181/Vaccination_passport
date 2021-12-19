namespace VaccinationPassportLibrary.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AmbCard { get; set; }
        public string Doctor { get; set; }
        public string Polyclinic { get; set; }
        public DateTime? DeclarationDate { get; set; }

        public override string ToString() => $"ID = {ID}, Full Name = {FullName}";

    }
}
