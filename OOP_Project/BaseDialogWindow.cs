using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    abstract class BaseDialogWindow : IWindow
    {
        public abstract void Show();
        public abstract void Close();
    }
}
