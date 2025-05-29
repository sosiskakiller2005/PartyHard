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
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        private PartyHardDbEntities _context = App.GetContext();
        public AddServiceWindow()
        {
            InitializeComponent();
        }

        private void AddServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ServiceNameTb.Text))
            {
                Services newService = new Services()
                {
                    ServiceName = ServiceNameTb.Text,
                };
                _context.Services.Add(newService);
                _context.SaveChanges();
                MessageBoxHelper.Information("Услуга успешно добавлена!");
                Close();
            }
            else
            {
                MessageBoxHelper.Error("Пожалуйста, заполните все поля перед добавлением услуги.", "Ошибка добавления услуги");
            }
        }
    }
}
