using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Category.Service.EF
{
    public partial class CategoryContext : DbContext
    {
        public CategoryContext()
        {
        }

        public CategoryContext(DbContextOptions<CategoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.\\SqlExpress;Trusted_Connection=True;Database=Category");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__tbl_Cate__D54EE9B4C5FD57A2");

                entity.ToTable("tbl_Categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("category");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
