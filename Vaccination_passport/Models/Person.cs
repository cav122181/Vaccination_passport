using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination_passport.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }
         public string AmbCard { get; set; }
        public string Doctor { get; set; }
        public string Polyclinic { get; set; }
        public DateTime? DeclDate { get; set; }

        public override string ToString()
        {
            return $"ID = {ID}, Full Name = {FullName}";
        }
    }
}
