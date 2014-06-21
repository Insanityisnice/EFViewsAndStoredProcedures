using OrderProcessing.Models.Entities;
using OrderProcessing.Models.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models
{
    public class OrderProcessingContext : DbContext, IOrderProcessingContext
    {
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderItem> OrderItems { get; set; }

        public OrderProcessingContext()
            : base("name=OrderProcessing")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
        }

        public void DeleteCustomer(int id)
        {
            Database.ExecuteSqlCommand("dbo.DeleteCustomer @id", new SqlParameter("@id", id));
        }

        public void Update<T>(T entity) where T : class
        {
            Entry<T>(entity).State = EntityState.Modified;
        }
    }
}