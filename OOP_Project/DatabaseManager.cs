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
        static string packagePath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";
        static string userPath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/UserDB.json";

        public static bool DelUser(string login)
        {

            if (!File.Exists(userPath)) return false;

            string existingData = File.ReadAllText(userPath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            var userToRemove = users.Find(u => u.login == login);
            if (userToRemove == null) return false;

            users.Remove(userToRemove);

            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userPath, jsonString);

            return true;
        }

        public static bool AddUser(User user)
        {
            List<User> users = new List<User>();

            if (string.IsNullOrWhiteSpace(user.login) || user.login.Length < 2) return false;

            if (user == null || user.login == null || user.Name == null) return false;

            if (user.password.Length < 4 || string.IsNullOrWhiteSpace(user.password)) return false;

            if (File.Exists(userPath))
            {
                string existingData = File.ReadAllText(userPath);
                if (!string.IsNullOrEmpty(existingData))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();
                }
            }

            if (users.Exists(u => u.login == user.login))
            {
                throw new Exception("Користувач вже існує");
            }

            users.Add(user);
            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userPath, jsonString);

            return true; 
        }

        public static bool AddPackage(Package package)
        {
            List<Package> packages = new List<Package>();
            List<User> users = new List<User>();

            if (package == null) return false;
            if (package.isDockument && package.weight > 0.1) return false;

            if (File.Exists(packagePath))
            {
                string existingPackages = File.ReadAllText(packagePath);
                if (!string.IsNullOrEmpty(existingPackages))
                {
                    packages = JsonSerializer.Deserialize<List<Package>>(existingPackages) ?? new List<Package>();
                }
            }

            if (packages.Exists(p => p.id == package.id))
            {
                return false;
            }

            if (File.Exists(userPath))
            {
                string existingUsers = File.ReadAllText(userPath);
                if (!string.IsNullOrEmpty(existingUsers))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingUsers) ?? new List<User>();
                }
            }

            User sender = users.Find(u => u.login == package.senderLogin);
            User receiver = users.Find(u => u.login == package.receiverLogin);

            if (sender == null || receiver == null)
            {
                return false;
            }

            if (!sender.packagesSends.Contains(package.id))
                sender.packagesSends.Add(package.id);

            if (!receiver.packagesReceive.Contains(package.id))
                receiver.packagesReceive.Add(package.id);

            string updatedUsersJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userPath, updatedUsersJson);

            packages.Add(package);
            string updatedPackagesJson = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(packagePath, updatedPackagesJson);

            return true;
        }

        public static User GetUserByLogin(string login)
        {
            string existingUsers = File.ReadAllText(userPath);
            List<User> users = new List<User>();

            if (!string.IsNullOrEmpty(existingUsers))
            {
                users = JsonSerializer.Deserialize<List<User>>(existingUsers) ?? new List<User>();
            }

            return users.FirstOrDefault(u => u.login == login);
        }

        public static List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public static List<Package> GetPackagesByUser(string userLogin)
        {
            string path = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";

            if (!File.Exists(path)) return new List<Package>();

            string existingData = File.ReadAllText(path);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            List<Package> result = packages.FindAll(p => p.senderLogin == userLogin || p.senderLogin == userLogin);

            return result;
        }

        public static bool UpdatePackage(Package package, Guid id)
        {
            string path = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";

            if (!File.Exists(path)) return false;

            string existingData = File.ReadAllText(path);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            int removedCount = packages.RemoveAll(p => p.id == id);

            if (removedCount == 0)
                return false;

            packages.Add(package);

            string updatedData = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, updatedData);

            return true;
        }
    }
}
