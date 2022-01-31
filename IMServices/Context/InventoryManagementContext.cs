using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using IMServices.Entities;

namespace IMServices.Context
{
    public partial class InventoryManagementContext : DbContext
    {
        public InventoryManagementContext()
        {
        }

        public InventoryManagementContext(DbContextOptions<InventoryManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("server=.\\SqlExpress;Trusted_Connection=True;Database=InventoryManagement;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl1)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl3)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl4)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl5)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl6)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MerchantId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDiscount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductTag)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.SizeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SizePrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserProf__1788CC4CE882460D");

                entity.ToTable("UserProfile");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePictureUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserCountry)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserFirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserLastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserState)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
