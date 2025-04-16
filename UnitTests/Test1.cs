using OOP_Project;
using System.Text.Json;

namespace UnitTests
{
    [TestClass]
    public sealed class Test1
    {
        private static readonly Mutex fileMutex = new Mutex();
        static string userDBPath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/UserDB.json";
        static string packageDBPath = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";

        [TestMethod]
        public void DatabaseAddUser()
        {
            fileMutex.WaitOne();

            try
            {
                User user = new("Julian_123", "123123", "Julian", "Whitmore", "Alexander");
                List<User> users = new List<User>();

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(userDBPath);
                users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

                Assert.IsTrue(users.Exists(u => u.login == user.login));
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void DatabaseAddUserWithWhiteLogin()
        {
            fileMutex.WaitOne();

            try
            {
                User user = new("", "123123", "", "", "");
                List<User> users = new List<User>();

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(userDBPath);
                users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

                Assert.IsFalse(users.Exists(u => u.login == user.login));
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void DatabaseAddUserWithNullLogin()
        {
            fileMutex.WaitOne();

            try
            {
                User user = new(null, "123123", null, null, null);
                List<User> users = new List<User>();

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(userDBPath);
                users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

                Assert.IsFalse(users.Exists(u => u.login == user.login));
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void DatabaseAddUserWithUncorrectLogin()
        {
            fileMutex.WaitOne();

            try
            {
                User user = new("h", "123123", "h", "p", "r");
                List<User> users = new List<User>();

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(userDBPath);
                users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

                Assert.IsFalse(users.Exists(u => u.login == user.login));
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void DatabaseDelUser()
        {
            fileMutex.WaitOne();

            try
            {
                User user = new("Yan49", "123123", "Yan", "Kovalenko", "Mikhailovich");

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                bool res = DatabaseManager.DelUser("Yan49");

                Assert.IsTrue(res);
            }
            finally { fileMutex?.ReleaseMutex();}
        }

        [TestMethod]
        public void DatabaseAddPackage()
        {
            fileMutex.WaitOne();

            try
            {
                User sender = new("Kein22", "123123", "Kein", "Darnell", "Everett");
                User receiver = new("Lukasss", "123123", "Lukas", "Reinhardt", "Johann");
                List<Package> packages = new List<Package>();

                try { DatabaseManager.AddUser(sender); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try { DatabaseManager.AddUser(receiver); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Package package = new(Guid.NewGuid(), sender.login, receiver.login, "в дорозі", 12, 1500, false, "Kharkiv", "Kyiv", 76);

                try { bool res = DatabaseManager.AddPackage(package); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(packageDBPath);
                packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

                Assert.IsTrue(packages.Exists(p => p.id == package.id));
            }
            finally { fileMutex?.ReleaseMutex(); }
        }

        [TestMethod]
        public void DatabaseAddWrongPackage()
        {
            fileMutex.WaitOne();

            try
            {
                User sender = new("Kein22", "123123", "Kein", "Darnell", "Everett");
                User receiver = new("Lukasss", "123123", "Lukas", "Reinhardt", "Johann");
                List<Package> packages = new List<Package>();

                try { DatabaseManager.AddUser(sender); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try { DatabaseManager.AddUser(receiver); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Package package = new(Guid.NewGuid(), sender.login, receiver.login, "в дорозі", 12, 1500, true, "Kharkiv", "Kyiv", 24);

                try { bool res = DatabaseManager.AddPackage(package); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(packageDBPath);
                packages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

                Assert.IsFalse(packages.Exists(p => p.id == package.id));
            }
            finally { fileMutex?.ReleaseMutex();     }
        }

        [TestMethod]
        public void DatabaseAddIsDockumentPackage()
        {
            fileMutex.WaitOne();

            try
            {
                User sender = new("Kein22", "123123", "Kein", "Darnell", "Everett");
                User receiver = new("Lukasss", "123123", "Lukas", "Reinhardt", "Johann");
                List<Package> packages = new List<Package>();

                try { DatabaseManager.AddUser(sender); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try { DatabaseManager.AddUser(receiver); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Package package = new(Guid.NewGuid(), sender.login, receiver.login, "в дорозі", 0, 500, true, "Kharkiv", "Kyiv", 24);

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
                Guid testId = Guid.NewGuid();
                Package initial = new(testId, "Kein22", "Lukasss", "в дорозі", 12, 1450, false, "Kharkiv", "Lviv", 1);
                DatabaseManager.AddPackage(initial);

                Package updated = new(testId, "Kein22", "Lukasss", "доставлено", 12, 1450, false, "Kharkiv", "Lviv", 1);
                bool res = DatabaseManager.UpdatePackage(updated, testId);
            }
            finally { fileMutex.ReleaseMutex(); }
        }

        [TestMethod]
        public void databaseGetPackagesByUserLogin()
        {
            fileMutex.WaitOne();

            try
            {
                string userLogin = "Loyd56";
                List<Package> existPackages = new List<Package>();

                string existingData = File.ReadAllText(packageDBPath);
                existPackages = JsonSerializer.Deserialize<List<Package>>(existingData) ?? new List<Package>();

                List<Package> packages = DatabaseManager.GetPackagesByUser(userLogin);

                Assert.IsNotNull(packages);
            }
            finally { fileMutex.ReleaseMutex();  }
        }

        [TestMethod]
        public void databaseGetUserById()
        {
            fileMutex.WaitOne();

            try
            {
                User res = DatabaseManager.GetUserByLogin("Julian_123");

                Assert.IsNotNull(res);
            }
            finally { fileMutex.ReleaseMutex(); }
        }

        [TestMethod]
        public void databaseGetAllUsers()
        {
            fileMutex.WaitOne();

            try
            {
                List<User> users = new List<User>();

                string existingData = File.ReadAllText(packageDBPath);
                users = JsonSerializer.Deserialize<List<User>>(existingData) ?? new List<User>();

                Assert.IsNotNull(users);
            }
            finally { fileMutex.ReleaseMutex();}
        }

        [TestMethod]
        public void CorrectRegistration()
        {
            fileMutex.WaitOne();

            try
            {
                DatabaseManager.DelUser("PabloTax");

                bool res = DatabaseManager.Register("PabloTax", "123123", "123123", "Pablo", "Morales", "Enriquez");

                Assert.IsTrue(res);
            }
            finally { fileMutex.ReleaseMutex();}
        }

        [TestMethod]
        public void RegistrationWithWhitePassword()
        {
            fileMutex.WaitOne();

            try
            {
                bool res = DatabaseManager.Register("PabloTax", "", "", "Pablo", "Morales", "Enriquez");

                Assert.IsFalse(res);
            }
            finally { fileMutex.ReleaseMutex();  }
        }

        [TestMethod]
        public void RegistrationWithNullPassword()
        {
            fileMutex.WaitOne();

            try
            {
                bool res = DatabaseManager.Register("PabloTax", null, null, "Pablo", "Morales", "Enriquez");

                Assert.IsFalse(res);
            }
            finally { fileMutex.ReleaseMutex(); }
        }

        [TestMethod]
        public void RegistrationWithWrongPassword()
        {
            fileMutex.WaitOne();

            try
            {
                bool res = DatabaseManager.Register("PabloTax", "123", "123", "Pablo", "Morales", "Enriquez");

                Assert.IsFalse(res);
            }
            finally { fileMutex.ReleaseMutex(); }
        }

        [TestMethod]
        public void CorrectAuthorisation()
        {
            fileMutex.WaitOne();

            try
            {
                bool res = DatabaseManager.Login("PabloTax", "123123");

                Assert.IsTrue(res);
            }
            finally { fileMutex.ReleaseMutex();}
        }
    }
}
