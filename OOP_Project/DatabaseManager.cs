using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class DatabaseManager : IPackageRepository, IUserRepository
    {
        public static bool DelUser(User user)
        {
            throw new NotImplementedException();
        }

        public static bool AddUser(User user)
        {
            string path = "Databases/UserDB.json";

            List<User> users = new List<User>();

            if (File.Exists(path))
            {
                string existingData = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(existingData))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();
                }
            }

            if (users.Exists(u => u.Id == user.Id))
            {
                return true;
            }

            users.Add(user);
            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, jsonString);

            return true; 
        }

        public static bool AddPackage(Package package)
        {
            throw new NotImplementedException();
        }

        public static User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public static List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public static List<Package> GetPackagesByUser(int usreId)
        {
            throw new NotImplementedException();
        }

        public static bool UpdatePackage(Package package)
        {
            throw new NotImplementedException();
        }
    }
}
