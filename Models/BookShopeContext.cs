using BookShop.Data;
using BookShop.Mapping;
using BookShop.Models.ViewModels;
using BookSope2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class BookShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=BookShopDB;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Author_BookMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new Order_BookMap());
            modelBuilder.ApplyConfiguration(new Book_TranslatorMap());
            modelBuilder.ApplyConfiguration(new Book_CategoryMap());
            modelBuilder.Query<ReadAllBook>().ToView("ReadAllBooks");
            modelBuilder.Entity<Book>().HasQueryFilter(b => (bool)!b.Delete);
            //modelBuilder.Entity<Book>().Property(a => a.Delete).HasDefaultValueSql("0");
            modelBuilder.Entity<Book>().Property(a => a.PublishDate).HasDefaultValueSql("SELECT CONVERT(datetime,GETDATE())");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Provice> Provices { get; set; }
        public DbSet<Author_Book> Author_Books { get; set; }
        public DbSet<Order_Book> Order_Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Translator> Translator { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book_Category> Book_Categories { get; set; }
        public DbSet<Book_Translator> Book_Translators { get; set; }
        public DbQuery<ReadAllBook> ReadAllBook { get; set; }
        [DbFunction("GetAllAouther", "dbo")]
        public static string GetAllAouther(int BookId)
        {

            throw new NotImplementedException();
        }
        [DbFunction("GetAllCategories", "dbo")]
        public static string GetAllCategories(int BookId)
        {

            throw new NotImplementedException();
        }
        [DbFunction("GetAllTranslators", "dbo")]
        public static string GetAllTranslators(int BookId)
        {

            throw new NotImplementedException();
        }
    }
    
}


