using OOP_Project;

namespace UnitTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void databaseAddUser()
        {
            User user = new(1, "Julian");

            bool res = DatabaseManager.AddUser(user);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void databaseAddPackage()
        {
            User sender = new(1, "Julian");
            User receiver = new(2, "Lukas");
            Package package = new(1, sender, receiver, "в дорозі", 12, 1500, false);

            bool res = DatabaseManager.AddPackage(package);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void databaseUpdatePackage()
        {
            User sender = new(1, "Julian");
            User receiver = new(2, "Lukas");
            Package package = new(1, sender, receiver, "в дорозі", 12, 1500, false);

            bool res = DatabaseManager.UpdatePackage(package);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void databaseGetPackagesByUser()
        {
            User sender = new(1, "Julian");
            User receiver_1 = new(2, "Lukas");
            User receiver_2 = new(3, "Pedro");
            User receiver_3 = new(4, "Logan");

            Package package_1 = new(1, sender, receiver_1, "в дорозі", 12, 1500, false);
            Package package_2 = new(2, sender, receiver_2, "в дорозі", 0, 500, true);
            Package package_3 = new(3, sender, receiver_3, "в дорозі", 2, 700, false);

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
    }
}
