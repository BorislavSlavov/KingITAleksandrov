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

namespace KingITAleksandrov
{
    /// <summary>
    /// Логика взаимодействия для ArendaPage.xaml
    /// </summary>
    public partial class ArendaPage : Page
    {
        private Pavilions pavilion;
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<Tenants> tenantsCollection { get; set; }
        public Tenants currentTenants { get; set; }

        public ArendaPage(Pavilions pavilion)
        {
            InitializeComponent();
            this.pavilion = pavilion;
            Start = DateTime.Today;
            Stop = DateTime.Today;
            tenantsCollection = KingITEntities.GetContext().Tenants.ToList(); 
            DataContext = this;

        }

        private void BtnArenda_Click(object sender, RoutedEventArgs e)
        {
            if (Start <= Stop && Start >= DateTime.Today)
            {
                bool bol = Start == DateTime.Today;
                KingITEntities.GetContext().RentOrBookPavilionInSC(!bol, pavilion.pavilionNumber, pavilion.shopCenterNumber, Start, Stop, currentTenants.tenantNumber, Authorization.Employee_number);
                MessageBox.Show(bol ? "Арендовано" : "Забронировано");

            }

        }
    }
}
