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
        int id;
        User sender;
        User receiver;
        string status;
        float weight;
        int cost;
        bool isDockument;

        public Package(int id, User sender, User receiver, string status, float weight, int cost, bool isDockument)
        {
            this.id = id;
            this.sender = sender;
            this.receiver = receiver;
            this.status = status;
            this.weight = weight;
            this.cost = cost;
            this.isDockument = isDockument;
        }
    }
}
