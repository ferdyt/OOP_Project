using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    interface IUserRepository
    {
        static abstract bool DelUser(string login);
        static abstract bool AddUser(User user);
        static abstract User GetUserByLogin(string login);
        static abstract List<User> GetAllUsers();
    }
}
