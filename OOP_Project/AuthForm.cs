using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class AuthForm : IAuthenticable
    {
        public static bool Login(string username, string password)
        {
            string userPath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/UserDB.json";

            if (username == null || password == null)
                return false;

            string existingData = File.ReadAllText(userPath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(existingData);

            var user = users.FirstOrDefault(u => u.login == username);

            if (user == null)
                return false;

            if (user.password != password)
                return false;

            return true;
        }

    }
}
