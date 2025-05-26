using PartyHard.AppData;
using PartyHard.Model;
using PartyHard.Views.Windows;
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

namespace PartyHard.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        private PartyHardDbEntities _context = App.GetContext();
        public AddOrderPage()
        {
            InitializeComponent();
            ClientCmb.ItemsSource = _context.Clients.ToList();
            ClientCmb.DisplayMemberPath = "FullName";
            EventTypeCmb.ItemsSource = _context.EventTypes.ToList();
            EventTypeCmb.DisplayMemberPath = "TypeName";
            ServiceCmb.ItemsSource = _context.Services.ToList();
            ServiceCmb.DisplayMemberPath = "ServiceName";
            MasterClassCmb.ItemsSource = _context.MasterClasses.ToList();
            MasterClassCmb.DisplayMemberPath = "MasterClassName";
            UserCmb.ItemsSource = _context.Users.ToList();
            UserCmb.DisplayMemberPath = "FullName";
            DateDp.SelectedDate = DateTime.Now;
        }

        private void AddClientHL_Click(object sender, RoutedEventArgs e)
        {
            AddCLientWindow addClientWindow = new AddCLientWindow();
            if (addClientWindow.ShowDialog() == true)
            {
                ClientCmb.ItemsSource =  App.GetContext().Clients.ToList();
                ClientCmb.DisplayMemberPath = "FullName";
                MessageBoxHelper.Information("Клиент успешно добавлен.");
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StartTime.Time > EndTime.Time || DateDp.SelectedDate <= DateTime.Now)
            {
                MessageBoxHelper.Error("Выберите правильную дату и время.");
            }
            else
            {
                if (!string.IsNullOrEmpty(NoteTb.Text))
                {
                    try
                    {
                        Orders newOrder = new Orders()
                        {
                            Clients = ClientCmb.SelectedItem as Clients,
                            EventTypes = EventTypeCmb.SelectedItem as EventTypes,
                            Services = ServiceCmb.SelectedItem as Services,
                            MasterClasses = MasterClassCmb.SelectedItem as MasterClasses,
                            Users = UserCmb.SelectedItem as Users,
                            OrderDate = (DateTime)DateDp.SelectedDate,
                            StartTime = new DateTime(DateDp.SelectedDate.Value.Year, DateDp.SelectedDate.Value.Month, DateDp.SelectedDate.Value.Day, StartTime.Time.Hour, StartTime.Time.Minute, 0),
                            EndTime = new DateTime(DateDp.SelectedDate.Value.Year, DateDp.SelectedDate.Value.Month, DateDp.SelectedDate.Value.Day, EndTime.Time.Hour, EndTime.Time.Minute, 0),
                            Address = Adress.Text,
                            Notes = NoteTb.Text,
                            Status = 0
                        };
                        try
                        {
                            _context.Orders.Add(newOrder);
                            _context.SaveChanges();
                            MessageBoxHelper.Information("Заказ успешно добавлен.");
                        }
                        catch (Exception)
                        {
                            MessageBoxHelper.Error("Ошибка при добавлении заказа.");
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        MessageBoxHelper.Error("Заполните все поля для ввода.");
                    }
                }
                else
                {
                    try
                    {
                        Orders newOrder = new Orders()
                        {
                            Clients = ClientCmb.SelectedItem as Clients,
                            EventTypes = EventTypeCmb.SelectedItem as EventTypes,
                            Services = ServiceCmb.SelectedItem as Services,
                            MasterClasses = MasterClassCmb.SelectedItem as MasterClasses,
                            Users = UserCmb.SelectedItem as Users,
                            OrderDate = (DateTime)DateDp.SelectedDate,
                            StartTime = new DateTime(DateDp.SelectedDate.Value.Year, DateDp.SelectedDate.Value.Month, DateDp.SelectedDate.Value.Day, StartTime.Time.Hour, StartTime.Time.Minute, 0),
                            EndTime = new DateTime(DateDp.SelectedDate.Value.Year, DateDp.SelectedDate.Value.Month, DateDp.SelectedDate.Value.Day, EndTime.Time.Hour, EndTime.Time.Minute, 0),
                            Address = Adress.Text,
                            Status = 0
                        };
                        try
                        {
                            _context.Orders.Add(newOrder);
                            _context.SaveChanges();
                            MessageBoxHelper.Information("Заказ успешно добавлен.");
                        }
                        catch (Exception)
                        {
                            MessageBoxHelper.Error("Ошибка при добавлении заказа.");
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        MessageBoxHelper.Error("Заполните все поля для ввода.");
                    }
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameHelper.selectedFrame.Navigate(new MenuPage());
        }
    }
}
