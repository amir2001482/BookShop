﻿// <auto-generated />
using System;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookShop.Migrations
{
    [DbContext(typeof(BookShopContext))]
    partial class BookShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookShop.Areas.Identity.Data.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("BookShop.Areas.Identity.Data.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaim");
                });

            modelBuilder.Entity("BookShop.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("Image");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("LastVisitDateTime");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BookShop.Areas.Identity.Data.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRole");
                });

            modelBuilder.Entity("BookShop.Models.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("AuthorID");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookShop.Models.Author_Book", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<int>("AuthorID");

                    b.HasKey("BookID", "AuthorID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Author_Books");
                });

            modelBuilder.Entity("BookShop.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Delete")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<string>("File");

                    b.Property<string>("ISBN");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<bool?>("IsPublish");

                    b.Property<int>("LanguageID");

                    b.Property<int>("NumOfPages");

                    b.Property<int>("Price");

                    b.Property<DateTime?>("PublishDate");

                    b.Property<int>("PublishYear");

                    b.Property<int>("PublisherID");

                    b.Property<int>("Stock");

                    b.Property<string>("Summary");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<short>("Weight");

                    b.HasKey("BookID");

                    b.HasIndex("LanguageID");

                    b.HasIndex("PublisherID");

                    b.ToTable("BookInfo");
                });

            modelBuilder.Entity("BookShop.Models.Book_Category", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<int>("CategoryID");

                    b.HasKey("BookID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Book_Categories");
                });

            modelBuilder.Entity("BookShop.Models.Book_Translator", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<int>("TranslatorID");

                    b.HasKey("BookID", "TranslatorID");

                    b.HasIndex("TranslatorID");

                    b.ToTable("Book_Translators");
                });

            modelBuilder.Entity("BookShop.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<int?>("ParentCategoryID");

                    b.HasKey("CategoryID");

                    b.HasIndex("ParentCategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookShop.Models.City", b =>
                {
                    b.Property<int>("CityID");

                    b.Property<string>("CityName")
                        .IsRequired();

                    b.Property<int?>("ProvinceID");

                    b.HasKey("CityID");

                    b.HasIndex("ProvinceID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BookShop.Models.Customer", b =>
                {
                    b.Property<string>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<int>("CityID1");

                    b.Property<int>("CityID2");

                    b.Property<string>("Image");

                    b.Property<string>("PostalCode1");

                    b.Property<string>("PostalCode2");

                    b.Property<string>("Tell");

                    b.HasKey("CustomerID");

                    b.HasIndex("CityID1");

                    b.HasIndex("CityID2");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BookShop.Models.Discount", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<DateTime?>("EndDate");

                    b.Property<byte>("Percent");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("BookID");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("BookShop.Models.Language", b =>
                {
                    b.Property<int>("LanguageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LanguageName")
                        .IsRequired();

                    b.HasKey("LanguageID");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("BookShop.Models.Order", b =>
                {
                    b.Property<string>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AmountPaid");

                    b.Property<DateTime>("BuyDate");

                    b.Property<string>("CustomerID");

                    b.Property<string>("DispatchNumber");

                    b.Property<int?>("OrderStatusID");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("OrderStatusID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookShop.Models.Order_Book", b =>
                {
                    b.Property<string>("OrderID");

                    b.Property<int>("BookID");

                    b.HasKey("OrderID", "BookID");

                    b.HasIndex("BookID");

                    b.ToTable("Order_Books");
                });

            modelBuilder.Entity("BookShop.Models.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OrderStatusName");

                    b.HasKey("OrderStatusID");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("BookShop.Models.Provice", b =>
                {
                    b.Property<int>("ProvinceID");

                    b.Property<string>("ProvinceName")
                        .IsRequired();

                    b.HasKey("ProvinceID");

                    b.ToTable("Provices");
                });

            modelBuilder.Entity("BookShop.Models.Publisher", b =>
                {
                    b.Property<int>("PublisherID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PublisherName")
                        .IsRequired();

                    b.HasKey("PublisherID");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("BookShop.Models.Translator", b =>
                {
                    b.Property<int>("TranslatorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Family")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TranslatorID");

                    b.ToTable("Translator");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BookShop.Areas.Identity.Data.ApplicationRoleClaim", b =>
                {
                    b.HasOne("BookShop.Areas.Identity.Data.ApplicationRole", "Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShop.Areas.Identity.Data.ApplicationUserRole", b =>
                {
                    b.HasOne("BookShop.Areas.Identity.Data.ApplicationRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShop.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShop.Models.Author_Book", b =>
                {
                    b.HasOne("BookShop.Models.Author", "Author")
                        .WithMany("Author_Books")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShop.Models.Book", "Book")
                        .WithMany("Author_Books")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShop.Models.Book", b =>
                {
                    b.HasOne("BookShop.Models.Language", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShop.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShop.Models.Book_Category", b =>
                {
                    b.HasOne("BookShop.Models.Book", "Book")
                        .WithMany("book_Categories")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShop.Models.Category", "Category")
                        .WithMany("book_Categories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShop.Models.Book_Translator", b =>
                {
                    b.HasOne("BookShop.Models.Book", "Book")
                        .WithMany("book_Tranlators")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShop.Models.Translator", "Translator")
                        .WithMany("book_Tranlators")
                        .HasForeignKey("TranslatorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShop.Models.Category", b =>
                {
                    b.HasOne("BookShop.Models.Category", "category")
                        .WithMany("categories")
                        .HasForeignKey("ParentCategoryID");
                });

            modelBuilder.Entity("BookShop.Models.City", b =>
                {
                    b.HasOne("BookShop.Models.Provice", "Provice")
                        .WithMany("City")
                        .HasForeignKey("ProvinceID");
                });

            modelBuilder.Entity("BookShop.Models.Customer", b =>
                {
                    b.HasOne("BookShop.Models.City", "city1")
                        .WithMany("Customers1")
                        .HasForeignKey("CityID1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShop.Models.City", "city2")
                        .WithMany("Customers2")
                        .HasForeignKey("CityID2")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BookShop.Models.Discount", b =>
                {
                    b.HasOne("BookShop.Models.Book", "Book")
                        .WithOne("Discount")
                        .HasForeignKey("BookShop.Models.Discount", "BookID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookShop.Models.Order", b =>
                {
                    b.HasOne("BookShop.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID");

                    b.HasOne("BookShop.Models.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusID");
                });

            modelBuilder.Entity("BookShop.Models.Order_Book", b =>
                {
                    b.HasOne("BookShop.Models.Book", "Book")
                        .WithMany("Order_Books")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookShop.Models.Order", "Order")
                        .WithMany("Order_Books")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BookShop.Areas.Identity.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BookShop.Areas.Identity.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BookShop.Areas.Identity.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
