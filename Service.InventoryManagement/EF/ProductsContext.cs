using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Product.Service.EF
{
    public partial class ProductsContext : DbContext
    {
        public ProductsContext()
        {
        }

        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;
        public virtual DbSet<TblProductImage> TblProductImages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.\\SqlExpress;Trusted_Connection=True;Database=Products");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.PdtId)
                    .HasName("PK__tbl_Prod__FB0B2ACF80A51BE1");

                entity.ToTable("tbl_Products");

                entity.Property(e => e.PdtId).HasColumnName("pdt_id");

                entity.Property(e => e.PdtCategoryId).HasColumnName("pdt_category_id");

                entity.Property(e => e.PdtDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pdt_desc");

                entity.Property(e => e.PdtDiscount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("pdt_discount");

                entity.Property(e => e.PdtName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pdt_name");

                entity.Property(e => e.PdtPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("pdt_price");

                entity.Property(e => e.PdtQuantity).HasColumnName("pdt_quantity");

                entity.Property(e => e.PdtTag)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pdt_tag");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_id");
            });

            modelBuilder.Entity<TblProductImage>(entity =>
            {
                entity.HasKey(e => new { e.PdtId, e.ImageUrl })
                    .HasName("PK__tbl_Prod__ADFAFCD47BA504F0");

                entity.ToTable("tbl_ProductImages");

                entity.Property(e => e.PdtId).HasColumnName("pdt_id");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("image_url");

                entity.HasOne(d => d.Pdt)
                    .WithMany(p => p.TblProductImages)
                    .HasForeignKey(d => d.PdtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tbl_Produ__pdt_i__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
