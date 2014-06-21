using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace OrderProcessing.Models.Entities.Configuration
{
    internal class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("CustomerView", "dbo");
            HasKey(p => p.Id);

            Property(p => p.Name).HasMaxLength(50).IsRequired();

            HasMany(p => p.Orders)
                .WithRequired(x => x.Customer)
                .Map(m => m.MapKey("CustomerId"));
        }
    }
}