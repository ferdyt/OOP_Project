using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    abstract class BaseWindow : IWindow
    {
        abstract public void Show();
        abstract public void Close();
    }
}
