using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class User
    {
        public int id { get; }
        public string login { get; }

        public User(int id, string login)
        {
            this.id = id;
            this.login = login;
        }
    }
}
