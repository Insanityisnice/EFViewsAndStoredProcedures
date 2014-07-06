using OrderProcessing.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Models.Context
{
    public interface IOrderProcessingContext : IDisposable
    {
        IDbSet<Customer> Customers { get; }
        IDbSet<Order> Orders { get; }
        IDbSet<OrderItem> OrderItems { get; }

        void Update<T>(T entity) where T : class;
        int SaveChanges();
    }
}
