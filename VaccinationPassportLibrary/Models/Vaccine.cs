using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationPassportLibrary.Models
{
    public class Vaccine
    {
        public int Id { get; set; } 
        public Disease Disease { get; set; }
        public string VaccineName { get; set; }
        public double Dose { get; set; }
    }
}
