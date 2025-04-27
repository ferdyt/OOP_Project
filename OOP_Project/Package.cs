using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP_Project
{
    public class Package
    {
        public Guid id { get; }
        public string senderLogin { get; }
        public string senderCity { get; }
        public string receiverLogin { get; }
        public string receiverCity { get; }
        public PackageStatus status { get; set; }
        public float weight { get; }
        public int cost { get; }
        public bool isDockument { get; }
        public int postOffice { get; }

        public Package(Guid id, string senderLogin, string receiverLogin, PackageStatus status, float weight, int cost, bool isDockument, string senderCity, string receiverCity, int postOffice)
        {
            this.id = id;
            this.senderLogin = senderLogin;
            this.receiverLogin = receiverLogin;
            this.status = status;
            this.weight = weight;
            this.cost = cost;
            this.isDockument = isDockument;
            this.senderCity = senderCity;
            this.receiverCity = receiverCity;
            this.postOffice = postOffice;
        }
    }
}
