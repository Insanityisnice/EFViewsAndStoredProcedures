using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models.Entities.Configuration
{
    internal class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            ToTable("OrderItemView", "dbo");
            HasKey(p => p.Id);

            Property(p => p.Name).HasMaxLength(50).IsRequired();
            Property(p => p.Description).HasMaxLength(255).IsOptional();
            Property(p => p.Quantity).IsRequired();
        }
    }
}