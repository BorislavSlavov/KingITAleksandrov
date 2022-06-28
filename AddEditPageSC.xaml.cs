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
using System.IO;
using Microsoft.Win32;

namespace KingITAleksandrov
{
    /// <summary>
    /// Логика взаимодействия для AddEditPageSC.xaml
    /// </summary>
    public partial class AddEditPageSC : Page
    {
        private ShopCenters _currentSC = new ShopCenters();
        public AddEditPageSC(ShopCenters selecteSC)
        {
            InitializeComponent();

            if (selecteSC != null)
                _currentSC = selecteSC;
   
            DataContext = _currentSC;
            ComboStatus.ItemsSource = KingITEntities.GetContext().ShopCenters.Select(p => p.status).Distinct().ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentSC.shopCenterName))
                errors.AppendLine("Укажите название ТЦ");
            if (_currentSC.price <= 0 || _currentSC.price == null)
                errors.AppendLine("Затраты на строительство ТЦ - неотрицательное число");
            if (_currentSC.numberOfStoreys <= 0 || _currentSC.numberOfStoreys == null)
                errors.AppendLine("Количество этажей - больше чем 0");
            if (_currentSC.valueAddedFactor <= 0 || _currentSC.valueAddedFactor == null)
                errors.AppendLine("КДС - больше или равен 0");
            if (string.IsNullOrWhiteSpace(_currentSC.city))
                errors.AppendLine("Укажите город");
            if (_currentSC.status == null)
                errors.AppendLine("Выберите статус");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentSC.shopCenterNumber == 0)
            {
                _currentSC.shopCenterNumber = KingITEntities.GetContext().ShopCenters.Select(x => x.shopCenterNumber).Max() + 1;
                KingITEntities.GetContext().ShopCenters.Add(_currentSC);
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

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BPM;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\Public\Image ТЦ";

            fileDialog.Title = "Выбор фото для ТЦ";

            if (fileDialog.ShowDialog() == true)
            {
                _currentSC.photo = File.ReadAllBytes(fileDialog.FileName);
            }
            MessageBox.Show("Выбор файла завершен!");
        }

        private void BtnPavilion_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PavilionsPage(_currentSC));
        }
    }
}
