using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class RegistrationService
    {
        public static bool Register(string username, string password, string confirm_password, string name, string lastName, string middleName)
        {
            if (password != confirm_password) return false;
            if (username == null || password == null || confirm_password == null) return false;

            User user = new User(username, password, name, lastName, middleName);
            bool result = UserRepository.AddUser(user);
            return result;
        }
    }
}
