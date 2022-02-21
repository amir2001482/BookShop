using BookShop.Models;
using BookSope2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mapping
{
    public class Order_BookMap : IEntityTypeConfiguration<Order_Book>
    {
        public void Configure(EntityTypeBuilder<Order_Book> builder)
        {
            builder
             .HasKey(t => new { t.OrderID, t.BookID });

            builder
                .HasOne(pt => pt.Book)
                .WithMany(p => p.Order_Books)
                .HasForeignKey(pt => pt.BookID);

            builder
                .HasOne(pt => pt.Order)
                .WithMany(t => t.Order_Books)
                .HasForeignKey(pt => pt.OrderID);
        }
    }
}
    
