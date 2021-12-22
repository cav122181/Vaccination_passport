using System.Windows.Controls;

namespace VaccinationPassportUI.User_Controls
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
