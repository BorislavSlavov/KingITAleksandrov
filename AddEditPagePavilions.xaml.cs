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
    /// Логика взаимодействия для AddEditPagePavilions.xaml
    /// </summary>
    public partial class AddEditPagePavilions : Page
    {
        private Pavilions _currentPavilion = new Pavilions();
        public AddEditPagePavilions(Pavilions selectedPavilion)
        {
            InitializeComponent();

            if (selectedPavilion != null)
                _currentPavilion = selectedPavilion;

            DataContext = _currentPavilion;
            ComboSC.ItemsSource = KingITEntities.GetContext().ShopCenters.ToList();
            ComboStatus.ItemsSource = KingITEntities.GetContext().Pavilions.Select(p => p.status).Distinct().ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPavilion.pavilionNumber))
                errors.AppendLine("Укажите номер павильона");
            if (_currentPavilion.costPerSquareMeter <= 0 || _currentPavilion.costPerSquareMeter == null)
                errors.AppendLine("Стоимость за кв. м. - неотрицательное число");
            if (_currentPavilion.stage <= 0 || _currentPavilion.stage == null)
                errors.AppendLine("Этаж - больше чем 0");
            if (_currentPavilion.valueAddedFactor < 0.1 || _currentPavilion.valueAddedFactor == null)
                errors.AppendLine("КДС - больше или равен 0.1");
            if (_currentPavilion.area <= 0 || _currentPavilion.area == null)
                errors.AppendLine("Площадь - больше чем 0");
            if (_currentPavilion.ShopCenters == null)
                errors.AppendLine("Выберите ТЦ");
            if (_currentPavilion.status == null)
                errors.AppendLine("Выберите статус");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPavilion.shopCenterNumber == 0)
            {
                KingITEntities.GetContext().Pavilions.Add(_currentPavilion);
            }

            try
            {
                KingITEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
