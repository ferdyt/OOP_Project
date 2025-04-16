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

            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.MiddleName)) return false;

            if (user.Name.Length < 2 || user.LastName.Length < 2 || user.MiddleName.Length < 2) return false;

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

            if (package.senderCity.Length < 2 || package.receiverCity.Length < 2 || string.IsNullOrWhiteSpace(package.senderCity) || string.IsNullOrWhiteSpace(package.receiverCity)) return false;

            if (package.postOffice <= 0) return true;

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

        public static User? GetUserByLogin(string login)
        {
            string existingUsers = File.ReadAllText(userPath);
            List<User> users = new List<User>();

            if (!string.IsNullOrEmpty(existingUsers))
            {
                users = JsonSerializer.Deserialize<List<User>>(existingUsers) ?? new List<User>();
            }

            var user = users.FirstOrDefault(u => u.login == login);

            return user;
        }

        public static List<Package> GetPackagesByUser(string userLogin)
        {
            if (!File.Exists(packagePath)) return new List<Package>();

            string existingData = File.ReadAllText(packagePath);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            List<Package> result = packages.FindAll(p => p.senderLogin == userLogin || p.senderLogin == userLogin);

            return result;
        }

        public static bool UpdatePackage(Package package, Guid id)
        {
            if (!File.Exists(packagePath)) return false;

            string existingData = File.ReadAllText(packagePath);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            int removedCount = packages.RemoveAll(p => p.id == id);

            if (removedCount == 0)
                return false;

            packages.Add(package);

            string updatedData = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(packagePath, updatedData);

            return true;
        }

        public static bool Login(string username, string password)
        {
            if (username == null || password == null)
                return false;

            string existingData = File.ReadAllText(userPath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(existingData);

            if (users == null)
                return false;

            var user = users.FirstOrDefault(u => u.login == username);

            if (user == null)
                return false;

            if (user.password != password)
                return false;

            return true;
        }

        public static bool Register(string username, string password, string confirm_password, string name, string lastName, string middleName)
        {
            if (password != confirm_password) return false;

            if (username == null || password == null || confirm_password == null) return false;

            User user = new User(username, password, name, lastName, middleName);
            bool result = DatabaseManager.AddUser(user);
            return result;
        }

        public static List<Package> GetPackages()
        {
            var packages = new List<Package>();

            if (!File.Exists(packagePath)) return packages;

            string existingData = File.ReadAllText(packagePath);
            packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            return packages;
        }

        public static List<User> GetUsers()
        {
            var users = new List<User>();

            if (!File.Exists(userPath)) return users;

            string existingData = File.ReadAllText(userPath);
            users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            return users;
        }
    }
}
