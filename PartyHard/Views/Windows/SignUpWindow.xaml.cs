using PartyHard.AppData;
using PartyHard.Model;
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
using System.Windows.Shapes;

namespace PartyHard.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private PartyHardDbEntities _context = App.GetContext();
        public SignUpWindow()
        {
            InitializeComponent();
            GenderCmb.ItemsSource = _context.Gender.ToList();
            GenderCmb.DisplayMemberPath = "Name";
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FullnameTb.Text) && !string.IsNullOrEmpty(PhoneNumberTb.Text) && !string.IsNullOrEmpty(LoginTb.Text) 
                && !string.IsNullOrEmpty(PasswordTb.Password) && BirthDayDp.SelectedDate != null && GenderCmb.SelectedItem != null)
            {
                Users newUser = new Users()
                {
                    FullName = FullnameTb.Text,
                    NumberPhone = PhoneNumberTb.Text,
                    Birthday = (DateTime)BirthDayDp.SelectedDate,
                    Login = LoginTb.Text,
                    Password = PasswordTb.Password,
                    Gender = GenderCmb.SelectedItem as Gender
                };
                _context.Users.Add(newUser);
                try
                {
                    _context.SaveChanges();
                    MessageBoxHelper.Information("Вы успешно зарегистрировались");
                    AuthorisationWindow authorisationWindow = new AuthorisationWindow();
                    authorisationWindow.Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBoxHelper.Error(ex.Message);
                }
            }
            else
            {
                MessageBoxHelper.Error("Заполните все поля");
            }
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorisationWindow authorisationWindow = new AuthorisationWindow();
            authorisationWindow.Show();
            Close();
        }
    }
}
