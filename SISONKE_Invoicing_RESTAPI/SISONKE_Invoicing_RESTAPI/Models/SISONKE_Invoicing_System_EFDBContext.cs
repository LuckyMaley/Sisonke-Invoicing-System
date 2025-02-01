using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SISONKE_Invoicing_RESTAPI.Models
{
    public partial class SISONKE_Invoicing_System_EFDBContext : DbContext
    {
        public SISONKE_Invoicing_System_EFDBContext()
        {
        }

        public SISONKE_Invoicing_System_EFDBContext(DbContextOptions<SISONKE_Invoicing_System_EFDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<EfUser> EfUsers { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("discounts");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Rate)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("rate");
            });

            modelBuilder.Entity<EfUser>(entity =>
            {
                entity.ToTable("ef_users");

                entity.Property(e => e.EfUserId).HasColumnName("ef_user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.IdentityUsername)
                    .HasMaxLength(100)
                    .HasColumnName("identity_username");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(e => e.DiscountId, "IX_discount_id");

                entity.HasIndex(e => e.EfUserId, "IX_ef_user_id");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("due_date");

                entity.Property(e => e.EfUserId).HasColumnName("ef_user_id");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasColumnName("invoice_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");

                entity.Property(e => e.Tax).HasColumnName("tax");

                entity.Property(e => e.TaxAmount).HasColumnName("tax_amount");

                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_dbo.Invoices_dbo.discounts_discount_id");

                entity.HasOne(d => d.EfUser)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.EfUserId)
                    .HasConstraintName("FK_dbo.Invoices_dbo.ef_users_ef_user_id");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.ToTable("invoice_items");

                entity.HasIndex(e => e.InvoiceId, "IX_invoice_id");

                entity.HasIndex(e => e.ProductId, "IX_product_id");

                entity.Property(e => e.InvoiceItemId).HasColumnName("invoice_item_id");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_price");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_dbo.invoice_items_dbo.Invoices_invoice_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_dbo.invoice_items_dbo.products_product_id");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasIndex(e => e.InvoiceId, "IX_invoice_id");

                entity.Property(e => e.NoteId).HasColumnName("note_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.InvoiceNotes).HasColumnName("invoice_notes");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_dbo.Notes_dbo.Invoices_invoice_id");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.HasIndex(e => e.InvoiceId, "IX_invoice_id");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(100)
                    .HasColumnName("payment_method");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_dbo.payments_dbo.Invoices_invoice_id");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .HasColumnName("description");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
