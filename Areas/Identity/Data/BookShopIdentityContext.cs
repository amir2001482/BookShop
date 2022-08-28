using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopIdentityContext : IdentityDbContext<ApplicationUser , ApplicationRole, string, IdentityUserClaim<string> , ApplicationUserRole , IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string> > 
    {
        public BookShopIdentityContext(DbContextOptions<BookShopIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("AspNetUserRole");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("AspNetRoleClaims");
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(Users => Users.Users).HasForeignKey(r => r.RoleId);
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(userRole => userRole.User)
                .WithMany(Users => Users.Roles).HasForeignKey(r => r.UserId);

        }
       
    }
}
