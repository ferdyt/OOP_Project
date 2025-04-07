using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class User
    {
        public Guid id { get; }
        public string login { get; }

        public User(Guid id, string login)
        {
            this.id = id;
            this.login = login;
        }
    }
}
