using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class LoginService
    {
        public static bool Login(string username, string password)
        {
            if (username == null || password == null)
                return false;

            string existingData = File.ReadAllText(UserRepository.userPath);
            List<User>? users = JsonSerializer.Deserialize<List<User>>(existingData);

            if (users == null)
                return false;

            var user = users.FirstOrDefault(u => u.login == username);

            if (user == null)
                return false;

            if (user.password != password)
                return false;

            return true;
        }
    }
}
