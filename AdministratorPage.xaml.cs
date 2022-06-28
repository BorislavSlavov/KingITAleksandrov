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
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        public AdministratorPage()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageAdmin(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var EmployeesForRemoving = DGridEmployees.SelectedItems.Cast<Employees>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {EmployeesForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KingITEntities.GetContext().Employees.RemoveRange(EmployeesForRemoving);
                    KingITEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridEmployees.ItemsSource = KingITEntities.GetContext().Employees.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageAdmin((sender as Button).DataContext as Employees));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                KingITEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridEmployees.ItemsSource = KingITEntities.GetContext().Employees.ToList();
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentEmployee = KingITEntities.GetContext().Employees.ToList();

            var Search = TBoxSearch.Text.ToLower();
            currentEmployee = currentEmployee.Where(p => p.employeeSurname.ToLower().Contains(Search)).ToList();
            DGridEmployees.ItemsSource = currentEmployee.OrderBy(p => p.employeeNumber).ToList();
        }
    }
}
