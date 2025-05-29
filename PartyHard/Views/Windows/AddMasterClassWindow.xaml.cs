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
    /// Логика взаимодействия для AddMasterClassWindow.xaml
    /// </summary>
    public partial class AddMasterClassWindow : Window
    {
        private PartyHardDbEntities _context = App.GetContext();
        public AddMasterClassWindow()
        {
            InitializeComponent();
        }

        private void AddMasterClassBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MasterClassNameTb.Text))
            {
                MasterClasses newMasterClass = new MasterClasses()
                {
                    MasterClassName = MasterClassNameTb.Text,
                };
                _context.MasterClasses.Add(newMasterClass);
                _context.SaveChanges();
                MessageBoxHelper.Information("Мастер-класс успешно добавлен!");
                Close();
            }
            else
            {
                MessageBoxHelper.Error("Пожалуйста, заполните все поля перед добавлением мастер-класса.", "Ошибка добавления мастер-класса");
            }
        }
    }
}
