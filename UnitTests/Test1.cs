using OOP_Project;
using System.Text.Json;

namespace UnitTests
{
    [TestClass]
    public sealed class Test1
    {
        private static readonly Mutex fileMutex = new Mutex();

        [TestMethod]
        public void DatabaseAddUser()
        {
            fileMutex.WaitOne();

            try
            {
                string path = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/UserDB.json";
                User user = new(1, "Julian");
                List<User> users = new List<User>();

                try { DatabaseManager.AddUser(user); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(path);
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
                User user = new(1, "Julian");

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
                string path = "C:/Users/Csgo2/source/repos/OOP_Project/OOP_Project/Databases/PackageDB.json";
                User sender = new(1, "Julian");
                User receiver = new(2, "Lukas");
                List<Package> packages = new List<Package>();

                try { DatabaseManager.AddUser(sender); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try { DatabaseManager.AddUser(receiver); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Package package = new(1, sender.id, receiver.id, "в дорозі", 12, 1500, false);

                try { bool res = DatabaseManager.AddPackage(package); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                string existingData = File.ReadAllText(path);
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
                User sender = new(1, "Julian");
                User receiver = new(2, "Lukas");
                Package package = new(1, sender.id, receiver.id, "в дорозі", 12, 1450, false);

                try { DatabaseManager.AddUser(sender); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try { DatabaseManager.AddUser(receiver); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                bool res = DatabaseManager.UpdatePackage(package);

                Assert.IsTrue(res);
            }
            finally { fileMutex.ReleaseMutex(); }
        }

        /*[TestMethod]
        public void databaseGetPackagesByUserId()
        {
            User sender = new(1, "Julian");
            User receiver_1 = new(2, "Lukas");
            User receiver_2 = new(3, "Pedro");
            User receiver_3 = new(4, "Logan");

            Package package_1 = new(1, sender, receiver_1, "в дорозі", 12, 1450, false);
            Package package_2 = new(2, sender, receiver_2, "в дорозі", 0, 500, true);
            Package package_3 = new(3, sender, receiver_3, "в дорозі", 2, 700, false);

            DatabaseManager.AddUser(sender);
            DatabaseManager.AddUser(receiver_1);
            DatabaseManager.AddUser(receiver_2);
            DatabaseManager.AddUser(receiver_3);

            DatabaseManager.AddPackage(package_1);
            DatabaseManager.AddPackage(package_2);
            DatabaseManager.AddPackage(package_3);

            List<Package> packages = DatabaseManager.GetPackagesByUser(1);

            Assert.AreEqual(packages[2], package_3);
        }

        [TestMethod]
        public void databaseGetUserById()
        {
            User user = new(1, "Julian");

            DatabaseManager.AddUser(user);

            User res = DatabaseManager.GetUserById(1);

            Assert.AreEqual(user, res);
        }

        [TestMethod]
        public void databaseGetAllUsers()
        {
            User user_1 = new(1, "Julian");
            User user_2 = new(2, "Lukas");
            User user_3 = new(3, "Pedro");
            List<User> users = new() { user_1, user_2, user_3 };


            DatabaseManager.AddUser(user_1);
            DatabaseManager.AddUser(user_2);
            DatabaseManager.AddUser(user_3);

            List<User> res = DatabaseManager.GetAllUsers();

            Assert.AreEqual(res, users);
        }

        [TestMethod]
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
