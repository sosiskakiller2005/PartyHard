using PartyHard;
using PartyHard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PartyHard.AppData
{
    internal class AuthorisationHelper
    {
        private static PartyHardDbEntities _context = App.GetContext();
        public static Users selectedUser;

        /// <summary>
        /// Авторизует пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Authorise(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBoxHelper.Error("Не все поля для ввода были заполнены.");
                return false;
            }

            selectedUser = _context.Users.FirstOrDefault(user => user.Login == login && user.Password == password);

            if (selectedUser != null)
            {
                return true;
            }
            else
            {
                MessageBoxHelper.Error("Неправильно введен логин или пароль.");
                return false;
            }
        }
    }
}