using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class RegisterForm
    {
        public static bool Register(string username, string password, string confirm_password, string name)
        {
            if (password != confirm_password) return false;

            if (username == null || password == null || confirm_password == null) return false;

            User user = new User(username, password, name);
            bool result = DatabaseManager.AddUser(user);
            return result;
        }
    }
}
