using System;
using System.Windows.Controls;

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
