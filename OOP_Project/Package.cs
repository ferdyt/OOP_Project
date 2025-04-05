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
        public int id { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string status { get; set; }
        public float weight { get; set; }
        public int cost { get; set; }
        public bool isDockument { get; set; }

        public Package(int id, int senderId, int receiverId, string status, float weight, int cost, bool isDockument)
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
