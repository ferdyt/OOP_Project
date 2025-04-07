using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    public class User
    {
        public string Name { get; set; }
        public string login { get; }

        public string password { get; }
        public List<Guid> packagesSends { get; set; } = new List<Guid>();
        public List<Guid> packagesReceive { get; set; } = new List<Guid>();

        public User(string login, string password, string name)
        {
            this.login = login;
            this.password = password;
            Name = name;
        }
    }
}
