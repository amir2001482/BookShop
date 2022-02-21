using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookSope2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopIdentityContext : IdentityDbContext<ApplicationUser , ApplicationRoles , string, IdentityUserClaim<string> , ApplicationUserRole , IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string> > 
    {
        public BookShopIdentityContext(DbContextOptions<BookShopIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRoles>().ToTable("AspNetRoles").ToTable("AppRoles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("AppUserRoles");
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(Users => Users.UserRoles).HasForeignKey(r => r.RoleId);
            modelBuilder.Entity<ApplicationUser>().ToTable("AppUsers");
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(userRole => userRole.User)
                .WithMany(Users => Users.userRoles).HasForeignKey(r => r.UserId);

        }
       
    }
}
