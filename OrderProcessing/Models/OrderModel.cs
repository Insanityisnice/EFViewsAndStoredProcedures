using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}