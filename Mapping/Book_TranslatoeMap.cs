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
    public class Book_TranslatorMap : IEntityTypeConfiguration<Book_Translator>
    {
        public void Configure(EntityTypeBuilder<Book_Translator> builder)
        {
            builder.HasKey(p => new { p.BookID, p.TranslatorID });
            builder
                .HasOne(b => b.Book)
                .WithMany(p => p.book_Tranlators)
                .HasForeignKey(f => f.BookID);


            builder
              .HasOne(b => b.Translator)
              .WithMany(p => p.book_Tranlators)
              .HasForeignKey(f => f.TranslatorID);

        }
    }
}
