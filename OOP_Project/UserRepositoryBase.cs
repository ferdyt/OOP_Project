using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    public abstract class UserRepositoryBase
    {
        public abstract bool DelUser(string login);
        public abstract bool AddUser(User user);
        public abstract User? GetUserByLogin(string login);
        public abstract List<User> GetUsers();
    }
}
