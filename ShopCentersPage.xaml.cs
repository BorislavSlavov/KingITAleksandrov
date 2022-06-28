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
    /// Логика взаимодействия для ShopCentersPage.xaml
    /// </summary>
    public partial class ShopCentersPage : Page
    {
        public ShopCentersPage()
        {
            InitializeComponent();

            ComboCity.ItemsSource = KingITEntities.GetContext().ShopCenters.Select(x => x.city).Distinct().ToList();
            ComboStatus.ItemsSource = KingITEntities.GetContext().ShopCenters.Select(x => x.status).Distinct().ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageSC(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var SCForRemoving = DGridShopCentres.SelectedItems.Cast<ShopCenters>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {SCForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KingITEntities.GetContext().ShopCenters.RemoveRange(SCForRemoving);
                    KingITEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridShopCentres.ItemsSource = KingITEntities.GetContext().ShopCenters.ToList();

                }                
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                KingITEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridShopCentres.ItemsSource = KingITEntities.GetContext().ShopCenters.ToList();
            }
        }


        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageSC((sender as Button).DataContext as ShopCenters));
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGridShopCentres.ItemsSource = KingITEntities.GetContext().ShopCenters.Where(b => b.status == ComboStatus.SelectedItem && b.status != "Удален").ToList();
        }

        private void ComboCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           DGridShopCentres.ItemsSource = KingITEntities.GetContext().ShopCenters.Where(b => b.city == ComboCity.SelectedItem && b.status != "Удален").ToList();
        }
        private void BtnPavilions_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PavilionsPage(null));
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ComboStatus.SelectedItem = null;
            ComboCity.SelectedItem = null;
            DGridShopCentres.ItemsSource = KingITEntities.GetContext().ShopCenters.ToList();
        }
    }

}
