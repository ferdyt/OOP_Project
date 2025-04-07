using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    interface IUserRepository
    {
        static abstract bool DelUser(User user);
        static abstract bool AddUser(User user);
        static abstract User GetUserById(Guid id);
        static abstract List<User> GetAllUsers();
    }
}
