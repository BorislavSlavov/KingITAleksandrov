using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace KingITAleksandrov
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public static int Employee_number = 1;
        string _login, _password;
        public int counterClick = 0;
        public Authorization()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Employees User = null;

            _login = LoginText.Text;
            _password = PasswordText.Password;
            User = KingITEntities.GetContext().Employees.Where(p => p.employeePassword == _password && p.employeeLogin == _login).FirstOrDefault();

            if (counterClick < 3)

                if (User == null || User.employeeRole == "Удален")
                {
                    counterClick++; MessageBox.Show("Не найдено");
                }
                else if (User.employeeRole == "Администратор")
                {
                    Employee_number = User.employeeNumber;
                    MessageBox.Show("Успешно");
                    _login = User.employeeLogin;
                    Manager.MainFrame.Content = new AdministratorPage();
                }
                else
                {
                    Employee_number = User.employeeNumber;
                    MessageBox.Show("Успешно");
                    _login = User.employeeLogin;
                    Manager.MainFrame.Content = new ShopCentersPage();
                }
            else
            {
                MessageBox.Show($"Попытки авторизации исчерпаны,пройдите капчу!");
                Manager.MainFrame.Navigate(new Captcha());
                counterClick = 0;
            }

        }
    }
}



