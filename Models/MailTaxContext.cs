using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MailTax.Models
{
    public partial class MailTaxContext : DbContext
    {
        public MailTaxContext()
        {
        }

        public MailTaxContext(DbContextOptions<MailTaxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Doc> Docs { get; set; } = null!;
        public virtual DbSet<Expense> Expenses { get; set; } = null!;
        public virtual DbSet<Folder> Folders { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=LAPTOP-0NEUB4M4\\SQLEXPRESS;Database=MailTax;Trusted_Connection=False;password=MailTax2022;user=Rachel;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<Doc>(entity =>
            {
                entity.ToTable("Doc");

                entity.HasIndex(e => e.DocPath, "UQ__Doc__DFFB64AE33A6DA26")
                    .IsUnique();

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.DocDesc).HasMaxLength(500);

                entity.Property(e => e.DocName).HasMaxLength(100);

                entity.Property(e => e.DocPath).HasMaxLength(250);

                entity.Property(e => e.FolderId).HasColumnName("FolderID");

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Docs)
                    .HasForeignKey(d => d.FolderId)
                    .HasConstraintName("FK__Doc__FolderID__276EDEB3");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expense");

                entity.Property(e => e.ExpenseDate).HasColumnType("date");

                entity.Property(e => e.ExpenseDescription).HasMaxLength(200);

                entity.Property(e => e.SupplierName).HasMaxLength(50);

                entity.HasOne(d => d.SupplierNameNavigation)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.SupplierName)
                    .HasConstraintName("FK__Expense__Supplie__300424B4");
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.ToTable("Folder");

                entity.Property(e => e.FolderId).HasColumnName("FolderID");

                entity.Property(e => e.FolderDesc).HasMaxLength(300);

                entity.Property(e => e.FolderName).HasMaxLength(100);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierName)
                    .HasName("PK__Supplier__9C5DF66E72AB14DD");

                entity.ToTable("Supplier");

                entity.Property(e => e.SupplierName).HasMaxLength(50);

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("SupplierID");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK__Supplier__Catego__2D27B809");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
