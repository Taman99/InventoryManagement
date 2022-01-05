using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sizes.Service.EF
{
    public partial class SizeContext : DbContext
    {
        public SizeContext()
        {
        }

        public SizeContext(DbContextOptions<SizeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblSize> TblSizes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.\\SqlExpress;Trusted_Connection=True;Database=Size");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblSize>(entity =>
            {
                entity.HasKey(e => e.SizeIndex)
                    .HasName("PK__tbl_Size__5D2D97ED56C5C627");

                entity.ToTable("tbl_Sizes");

                entity.Property(e => e.SizeIndex)
                    .ValueGeneratedNever()
                    .HasColumnName("size_index");

                entity.Property(e => e.PdtId).HasColumnName("pdt_id");

                entity.Property(e => e.Size)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("size");

                entity.Property(e => e.SizePrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("size_price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
