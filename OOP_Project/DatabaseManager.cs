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

        public static bool DelUser(User user)
        {

            if (!File.Exists(userPath)) return false;

            string existingData = File.ReadAllText(userPath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            var userToRemove = users.Find(u => u.id == user.id);
            if (userToRemove == null) return false;

            users.Remove(userToRemove);

            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userPath, jsonString);

            return true;
        }

        public static bool AddUser(User user)
        {
            List<User> users = new List<User>();

            if (File.Exists(userPath))
            {
                string existingData = File.ReadAllText(userPath);
                if (!string.IsNullOrEmpty(existingData))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();
                }
            }

            if (users.Exists(u => u.id == user.id))
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

            User sender = users.Find(u => u.id == package.senderId);
            User receiver = users.Find(u => u.id == package.receiverId);

            if (sender == null || receiver == null)
            {
                return false;
            }

            packages.Add(package);
            string updatedPackagesJson = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(packagePath, updatedPackagesJson);

            return true;
        }

        public static User GetUserById(Guid id)
        {
            string existingUsers = File.ReadAllText(userPath);
            List<User> users = new List<User>();

            if (!string.IsNullOrEmpty(existingUsers))
            {
                users = JsonSerializer.Deserialize<List<User>>(existingUsers) ?? new List<User>();
            }

            return users.FirstOrDefault(u => u.id == id);
        }

        public static List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public static List<Package> GetPackagesByUser(Guid userId)
        {
            string path = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";

            if (!File.Exists(path)) return new List<Package>();

            string existingData = File.ReadAllText(path);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            List<Package> result = packages.FindAll(p => p.senderId == userId || p.senderId == userId);

            return result;
        }

        public static bool UpdatePackage(Package package)
        {
            string path = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";

            if (!File.Exists(path)) return false;

            string existingData = File.ReadAllText(path);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();
            Package packageToDel = null;

            foreach (Package p in packages)
            {
                if (p.id == package.id)
                {
                    packageToDel = p;
                }
            }

            if (packageToDel != null)
            {
                packages.Remove(packageToDel);
            }

            packages.Add(package);

            string updatePackage = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, updatePackage);

            return true;
        }
    }
}
