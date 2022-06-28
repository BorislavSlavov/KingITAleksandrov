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
    /// Логика взаимодействия для AddEditPageAdmin.xaml
    /// </summary>
    public partial class AddEditPageAdmin : Page
    {
        private Employees _currentEmployees = new Employees();
        public AddEditPageAdmin(Employees selectedEmployees)
        {
            InitializeComponent();
            if (selectedEmployees != null)
                _currentEmployees = selectedEmployees;

            DataContext = _currentEmployees;
            ComboRole.ItemsSource = KingITEntities.GetContext().Employees.Select(p => p.employeeRole).Distinct().ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentEmployees.employeeSurname))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(_currentEmployees.employeeFirstname))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(_currentEmployees.employeeSecondname))
                errors.AppendLine("Укажите отчетсво");
            if (string.IsNullOrWhiteSpace(_currentEmployees.employeeLogin))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(_currentEmployees.employeePassword))
                errors.AppendLine("Укажите пароль");
            if (string.IsNullOrWhiteSpace(_currentEmployees.phoneNumber))
                errors.AppendLine("Укажите Номер телефона");
            if (_currentEmployees.employeeRole == null)
                errors.AppendLine("Выберите роль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentEmployees.employeeNumber == 0)
            {
                _currentEmployees.employeeNumber = KingITEntities.GetContext().Employees.Select(x => x.employeeNumber).Max() + 1;
                KingITEntities.GetContext().Employees.Add(_currentEmployees);
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
