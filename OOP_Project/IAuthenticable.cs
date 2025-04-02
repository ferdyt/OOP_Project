using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    interface IAuthenticable
    {
        abstract static bool Login(string username, string password);
    }
}
