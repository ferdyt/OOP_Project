using System.IO;
using System.Text.Json;
using System.Windows;

namespace OOP_Project
{
    public class PackageRepository : IPackageRepository
    {
        public static bool AddPackage(Package package)
        {
            List<Package> packages = new List<Package>();
            List<User> users = new List<User>();

            if (!PackageValidator.Validate(package))
                return false;

            if (File.Exists(Path.packagePath))
            {
                string existingPackages = File.ReadAllText(Path.packagePath);
                if (!string.IsNullOrEmpty(existingPackages))
                {
                    packages = JsonSerializer.Deserialize<List<Package>>(existingPackages) ?? new List<Package>();
                }
            }

            if (packages.Exists(p => p.id == package.id))
            {
                return false;
            }

            if (File.Exists(Path.userPath))
            {
                string existingUsers = File.ReadAllText(Path.userPath);
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
            File.WriteAllText(Path.userPath, updatedUsersJson);

            packages.Add(package);
            string updatedPackagesJson = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path.packagePath, updatedPackagesJson);

            return true;
        }

        public static List<Package> GetPackagesByUser(string userLogin)
        {
            if (!File.Exists(Path.packagePath)) return new List<Package>();

            string existingData = File.ReadAllText(Path.packagePath);

            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            Predicate<Package> predicate = p => p.senderLogin == userLogin || p.receiverLogin == userLogin;
            List<Package> result = packages.FindAll(predicate);

            return result;
        }


        public static bool UpdatePackage(Package package, Guid id)
        {
            if (!File.Exists(Path.packagePath)) return false;

            string existingData = File.ReadAllText(Path.packagePath);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            Predicate<Package> predicate = p => p.id == id;
            int removedCount = packages.RemoveAll(predicate);

            if (removedCount == 0)
                return false;

            packages.Add(package);

            string updatedData = JsonSerializer.Serialize(packages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path.packagePath, updatedData);

            return true;
        }

        public static List<Package> GetPackages()
        {
            var packages = new List<Package>();
            if (!File.Exists(Path.packagePath)) return packages;

            string existingData = File.ReadAllText(Path.packagePath);
            packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            return packages;
        }

        public static Package? GetPackageById(Guid id)
        {
            if (!File.Exists(Path.packagePath))
                return null;

            string existingData = File.ReadAllText(Path.packagePath);
            List<Package> packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();
            
            Func<Package, bool> predicate = p => p.id == id;
            var package = packages.FirstOrDefault(predicate);

            return package;
        }
    }
}
