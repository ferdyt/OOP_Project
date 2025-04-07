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
        public Guid id { get;}
        public Guid senderId { get; }
        public Guid receiverId { get; }
        public string status { get; }
        public float weight { get; }
        public int cost { get; }
        public bool isDockument { get; }

        public Package(Guid id, Guid senderId, Guid receiverId, string status, float weight, int cost, bool isDockument)
        {
            this.id = id;
            this.senderId = senderId;
            this.receiverId = receiverId;
            this.status = status;
            this.weight = weight;
            this.cost = cost;
            this.isDockument = isDockument;
        }
    }
}
