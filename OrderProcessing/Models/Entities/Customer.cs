using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}