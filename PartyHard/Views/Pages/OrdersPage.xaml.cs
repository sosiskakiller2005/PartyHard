using PartyHard.AppData;
using PartyHard.Model;
using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private PartyHardDbEntities _context = App.GetContext();
        private List<Orders> _ordersList;
        public OrdersPage()
        {
            InitializeComponent();
            _ordersList = _context.Orders.ToList();
            OrdersLv.ItemsSource = _ordersList;
            List<string> sortList = new List<string>
            {
                "По убыванию даты",
                "По возрастанию даты",
                "По возрастанию цены",
                "По убыванию цены"
            };
            SortCmb.ItemsSource = sortList;
            SortCmb.SelectedIndex = 1;
            List<Clients> clients = _context.Clients.ToList();
            clients.Insert(0, new Clients { FullName = "Все клиенты" });
            FilterByClientCmb.ItemsSource = clients;
            FilterByClientCmb.DisplayMemberPath = "FullName";
            List<Users> users = _context.Users.ToList();
            users.Insert(0, new Users { FullName = "Все пользователи" });
            FilterByUserCmb.ItemsSource = users;
            FilterByUserCmb.DisplayMemberPath = "FullName";
            FilterByClientCmb.SelectedIndex = 0;
            FilterByUserCmb.SelectedIndex = 0;
        }

        private void FilterByClientCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrdersLv.ItemsSource = Filter();
        }

        private void FilterByUserCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrdersLv.ItemsSource = Filter();
        }

        private List<Orders> Filter()
        {
            List<Orders> filteredOrders = _ordersList;
            Clients selectedClient = FilterByClientCmb.SelectedItem as Clients;
            Users selectedUser = FilterByUserCmb.SelectedItem as Users;
            if (selectedClient != null && selectedUser != null)
            {
                if (FilterByClientCmb.SelectedIndex == 0 && FilterByUserCmb.SelectedIndex == 0)
                {
                    filteredOrders = _ordersList;
                }
                else if (FilterByClientCmb.SelectedIndex != 0 && FilterByUserCmb.SelectedIndex == 0)
                {
                    filteredOrders = _ordersList.Where(o => o.Clients.FullName == selectedClient.FullName).ToList();
                }
                else if (FilterByClientCmb.SelectedIndex == 0 && FilterByUserCmb.SelectedIndex != 0)
                {
                    filteredOrders = _ordersList.Where(o => o.Users.FullName == selectedUser.FullName).ToList();
                }
                else
                {
                    filteredOrders = _ordersList.Where(o => o.Clients.FullName == selectedClient.FullName && o.Users.FullName == selectedUser.FullName).ToList();
                }
                return filteredOrders;
            }
            else
            {
                return null ;
            }
        }

        private void SortCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SortCmb.SelectedIndex)
            {
                case 0: // По возрастанию даты
                    OrdersLv.ItemsSource = _context.Orders.OrderBy(o => o.OrderDate).ToList();
                    break;
                case 1: // По убыванию даты
                    OrdersLv.ItemsSource = _context.Orders.OrderByDescending(o => o.OrderDate).ToList();
                    break;
                case 2: // По возрастанию цены
                    OrdersLv.ItemsSource = _context.Orders.OrderBy(o => o.Services.Amount).ToList();
                    break;
                case 3: // По убыванию цены
                    OrdersLv.ItemsSource = _context.Orders.OrderByDescending(o => o.Services.Amount).ToList();
                    break;
                default:
                    break;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameHelper.selectedFrame.Navigate(new MenuPage());
        }
    }
}
