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
    /// Логика взаимодействия для PavilionsPage.xaml
    /// </summary>
    public partial class PavilionsPage : Page
    {
        int currentSC_number, reg = 0;
        public PavilionsPage(ShopCenters currentSC) 
        {
            InitializeComponent();

            if (currentSC != null)
            {
                reg = 1;
                currentSC_number = currentSC.shopCenterNumber;
                DGridPavilions.ItemsSource = KingITEntities.GetContext().Pavilions.Where(x => x.shopCenterNumber == currentSC.shopCenterNumber).ToList();
            }
            else
            {
                DGridPavilions.ItemsSource = KingITEntities.GetContext().Pavilions.Where(x => x.status != "Удален").ToList();
            }
            ComboStatus.ItemsSource = KingITEntities.GetContext().Pavilions.Select(x => x.status).Distinct().ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPagePavilions(null));
        }
            
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var PavilionsForRemoving = DGridPavilions.SelectedItems.Cast<Pavilions>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {PavilionsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KingITEntities.GetContext().Pavilions.RemoveRange(PavilionsForRemoving);
                    KingITEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridPavilions.ItemsSource = KingITEntities.GetContext().Pavilions.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPagePavilions((sender as Button).DataContext as Pavilions));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                KingITEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                //DGridPavilions.ItemsSource = KingITEntities.GetContext().Pavilions.ToList();
            }
        }

        private void BtnArenda_Click(object sender, RoutedEventArgs e)
        {
            var rent = DGridPavilions.SelectedItems.Cast<Pavilions>().FirstOrDefault();
            Manager.MainFrame.Navigate(new ArendaPage(rent));
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reg == 1)
                DGridPavilions.ItemsSource = KingITEntities.GetContext().Pavilions.Where(b => b.status.ToString() == ComboStatus.SelectedItem.ToString() && b.shopCenterNumber == currentSC_number).ToList();
            else
                DGridPavilions.ItemsSource = KingITEntities.GetContext().Pavilions.Where(b => b.status.ToString() == ComboStatus.SelectedItem.ToString()).ToList();
        }

    }
}
