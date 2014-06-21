using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}