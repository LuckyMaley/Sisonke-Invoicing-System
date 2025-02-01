using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pocos
{
	public partial class Model1 : DbContext
	{
		public Model1()
			: base("name=Model1")
		{
		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EFUser>().Property(c => c.FirstName).IsRequired();
			modelBuilder.Entity<EFUser>().Property(c => c.LastName).IsRequired();
			modelBuilder.Entity<EFUser>().Property(c => c.Email).IsRequired();
			modelBuilder.Entity<EFUser>().Property(c => c.IdentityUserName).IsRequired();
			modelBuilder.Entity<EFUser>().Property(c => c.Role).IsRequired();
		}

		public virtual DbSet<EFUser> EFUsers { get; set; }
		public virtual DbSet<Discount> Discounts { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<Invoice> Invoices { get; set; }
		public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
		public virtual DbSet<Note> Notes { get; set; }
		

	}
}
