using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class DatabaseManager : IPackageRepository, IUserRepository
    {
        public static bool AddUser(User user)
        {
            throw new NotImplementedException();
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
