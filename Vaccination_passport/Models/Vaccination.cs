using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination_passport.Models
{
    internal class Vaccination
    {
        public int ID { get; set; }
        public Person Person { get; set; }
        public Disease Disease { get; set; } 
        public DateTime? VacDate { get; set; }
        public string Nurse { get; set; }
        public string VacName { get; set; }
        public float Dose { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string SerialNumber { get; set; }
        public string SideEff { get; set; }
    }
}
