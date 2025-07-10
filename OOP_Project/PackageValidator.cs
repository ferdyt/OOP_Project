using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace OOP_Project
{
    public static class PackageValidator
    {
        public static bool Validate(Package package)
        {
            if (package == null)
                return false;

            if (package.isDockument && package.weight > 0.1)
                return false;

            if (string.IsNullOrWhiteSpace(package.senderCity) || package.senderCity.Length < 2)
                return false;

            if (string.IsNullOrWhiteSpace(package.receiverCity) || package.receiverCity.Length < 2)
                return false;

            if (package.postOffice <= 0)
                return false;

            return true;
        }
    }
}
