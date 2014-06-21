using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models.Entities.Configuration
{
    internal class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            ToTable("OrderView", "dbo");
            HasKey(p => p.Id);

            Property(p => p.OrderNumber).HasMaxLength(20).IsRequired();
            Property(p => p.CreatedDate).IsRequired();
            Property(p => p.LastUpdatedDate).IsRequired();

            HasRequired(p => p.Customer)
                .WithMany(x => x.Orders)
                .Map(m => m.MapKey("CustomerId"));
        }
    }
}