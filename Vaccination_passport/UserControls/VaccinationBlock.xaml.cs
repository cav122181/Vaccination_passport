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
    /// Interaction logic for VaccinationBlock.xaml
    /// </summary>
    public partial class VaccinationBlock : UserControl
    {
        public VaccinationBlock()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        
        public string Title { get; set; }
    }
}
