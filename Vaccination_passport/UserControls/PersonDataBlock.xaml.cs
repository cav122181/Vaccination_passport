using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vaccination_passport.User_Controls
{
    /// <summary>
    /// Interaction logic for PersonDataBlock.xaml
    /// </summary>
    public partial class PersonDataBlock : UserControl
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }
        public string AmbCard { get; set; }
        public string Doctor { get; set; }
        public string Polyclinic { get; set; }
        public DateTime? DeclDate { get; set; }

        public PersonDataBlock()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
