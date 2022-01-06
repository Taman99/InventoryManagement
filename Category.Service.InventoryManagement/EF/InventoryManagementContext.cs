using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Category.Service.EF
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
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.\\SqlExpress;Trusted_Connection=True;Database=InventoryManagement;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Category1)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Category");
            });

            modelBuilder.Entity<Product>(entity =>
            {
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

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ImageUrl })
                    .HasName("PK__ProductI__677E08AD5F1038C4");

                entity.Property(e => e.ImageUrl).HasMaxLength(255);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductIm__Produ__38996AB5");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.SizeIndex)
                    .HasName("PK__Sizes__9633E3BB4F655567");

                entity.Property(e => e.SizeIndex).ValueGeneratedNever();

                entity.Property(e => e.Size1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Size");

                entity.Property(e => e.SizePrice).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
