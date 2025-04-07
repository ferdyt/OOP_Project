using OOP_Project;
using System.Text.Json;

namespace UnitTests
{
    [TestClass]
    public sealed class Test1
    {
        Guid packageGuid = Guid.NewGuid();
        private static readonly Mutex fileMutex = new Mutex();
        static string userDBPath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/UserDB.json";
        static string packageDBPath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";

        [TestMethod]
        public void DatabaseAddUser()
        {
            fileMutex.WaitOne();

            try
            {
                User user = new(Guid.NewGuid(), "Julian");
                List<User> users = new List<User>();

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(userDBPath);
                users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

                Assert.IsTrue(users.Exists(u => u.id == user.id));
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void DatabaseDelUser()
        {
            fileMutex.WaitOne();

            try
            {
                User user = new(Guid.NewGuid(), "Yan");

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                bool res = DatabaseManager.DelUser(user);

                Assert.IsTrue(res);
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void DatabaseAddPackage()
        {
            fileMutex.WaitOne();

            try
            {
                User sender = new(Guid.NewGuid(), "Kein");
                User receiver = new(Guid.NewGuid(), "Lukas");
                List<Package> packages = new List<Package>();

                try { DatabaseManager.AddUser(sender); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try { DatabaseManager.AddUser(receiver); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Package package = new(packageGuid, sender.id, receiver.id, "в дорозі", 12, 1500, false);

                try { bool res = DatabaseManager.AddPackage(package); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(packageDBPath);
                packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

                Assert.IsTrue(packages.Exists(p => p.id == package.id));
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void databaseUpdatePackage()
        {
            fileMutex.WaitOne();

            try
            {
                User sender = new(Guid.NewGuid(), "Loyd");
                User receiver = new(Guid.NewGuid(), "Hank");
                Package package = new(packageGuid, sender.id, receiver.id, "в дорозі", 12, 1450, false);

                try { DatabaseManager.AddUser(sender); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try { DatabaseManager.AddUser(receiver); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                bool res = DatabaseManager.UpdatePackage(package);

                Assert.IsTrue(res);
            }
            finally { fileMutex.ReleaseMutex(); }
        }

        [TestMethod]
        public void databaseGetPackagesByUserId()
        {
            Guid userId = Guid.Parse("69a0dcd0-1901-41e4-9fdd-72cf816e65ea");
            Guid packageId = Guid.Parse("13b187b0-069e-456d-a14d-029d23c3a9cc");
            List<Package> existPackages = new List<Package>();

            string existingData = File.ReadAllText(packageDBPath);
            existPackages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

            List<Package> packages = DatabaseManager.GetPackagesByUser(userId);

            Assert.IsTrue(existPackages.Exists(p => p.id == packageId) && packages.Exists(p => p.senderId == userId));
        }

        [TestMethod]
        public void databaseGetUserById()
        {
            Guid userId = Guid.Parse("69a0dcd0-1901-41e4-9fdd-72cf816e65ea");

            User res = DatabaseManager.GetUserById(userId);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void databaseGetAllUsers()
        {
            List<User> users = new List<User>();

            string existingData = File.ReadAllText(packageDBPath);
            users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

            Assert.IsNotNull(users);
        }

        /*[TestMethod]
        public void Registration()
        {
            bool res = RegisterForm.Register("Pablo", "123123", "123123");

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Authorisation()
        {
            RegisterForm.Register("Pablo", "123123", "123123");

            bool res = AuthForm.Login("Pablo", "123123");

            Assert.IsTrue(res);
        }
        */
    }
}
