using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination_passport.Models
{
    internal class Disease
    {
        public int ID { get; set; }
        public string DisName { get; set; }
        public int VaccineNumber { get; set; }
    }
}
