using PartyHard.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PartyHard
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static PartyHardDbEntities _context;
        public static PartyHardDbEntities GetContext()
        {
            if (_context == null)
            {
                _context = new PartyHardDbEntities();
            }
            return _context;
        }
    }
}
