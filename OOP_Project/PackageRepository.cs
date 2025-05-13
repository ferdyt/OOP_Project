using System.IO;
using System.Text.Json;

namespace OOP_Project
{
    public class PackageRepository : IPackageRepository
    {
        public static string packagePath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";

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

            if (File.Exists(UserRepository.userPath))
            {
                string existingUsers = File.ReadAllText(UserRepository.userPath);
                if (!string.IsNullOrEmpty(existingUsers))
                {
                    users = JsonSerializer.Deserialize<List<User>>(existingUsers) ?? new List<User>();
                }
            }

            Predicate<User> senderPredicate = u => u.login == package.senderLogin;
            User? sender = users.Find(senderPredicate);

            Predicate<User> receiverPredicate = u => u.login == package.receiverLogin;
            User? receiver = users.Find(receiverPredicate);

            if (sender == null || receiver == null)
            {
                return false;
            }

            if (!sender.packagesSends.Contains(package.id))
                sender.packagesSends.Add(package.id);

            if (!receiver.packagesReceive.Contains(package.id))
                receiver.packagesReceive.Add(package.id);

            string updatedUsersJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UserRepository.userPath, updatedUsersJson);

            packages.Add(package);
            string updatedPackagesJson = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(packagePath, updatedPackagesJson);

            return true;
        }

        public static List<Package> GetPackagesByUser(string userLogin)
        {
            if (!File.Exists(packagePath)) return new List<Package>();

            string existingData = File.ReadAllText(packagePath);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            Predicate<Package> predicate = p => p.senderLogin == userLogin || p.receiverLogin == userLogin;
            List<Package> result = packages.FindAll(predicate);

            return result;
        }


        public static bool UpdatePackage(Package package, Guid id)
        {
            if (!File.Exists(packagePath)) return false;

            string existingData = File.ReadAllText(packagePath);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            Predicate<Package> predicate = p => p.id == id;
            int removedCount = packages.RemoveAll(predicate);

            if (removedCount == 0)
                return false;

            packages.Add(package);

            string updatedData = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(packagePath, updatedData);

            return true;
        }

        public static List<Package> GetPackages()
        {
            var packages = new List<Package>();
            if (!File.Exists(packagePath)) return packages;

            string existingData = File.ReadAllText(packagePath);
            packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            return packages;
        }

        public static Package? GetPackageById(Guid id)
        {
            if (!File.Exists(packagePath))
                return null;

            string existingData = File.ReadAllText(packagePath);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();
            
            Func<Package, bool> predicate = p => p.id == id;
            var package = packages.FirstOrDefault(predicate);

            return package;
        }
    }
}
