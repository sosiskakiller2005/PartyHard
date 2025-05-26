using PartyHard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddCLientWindow.xaml
    /// </summary>
    public partial class AddCLientWindow : Window
    {
        private PartyHardDbEntities _context = App.GetContext();
        public AddCLientWindow()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTb.Text) && !string.IsNullOrEmpty(PhoneTb.Text) && !string.IsNullOrEmpty(EmailTb.Text) )
            {
                Clients newClient = new Clients()
                {
                    FullName = NameTb.Text,
                    NumberPhone = PhoneTb.Text,
                    Email = EmailTb.Text
                };
                _context.Clients.Add(newClient);
                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Клиент успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PhoneTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^\d+$");
        }
    }
}
