using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project
{
    interface IPackageRepository
    {
        static abstract bool AddPackage(Package package);
        static abstract List<Package> GetPackagesByUser(string userLogin);
        static abstract bool UpdatePackage(Package package, Guid id);
    }
}
