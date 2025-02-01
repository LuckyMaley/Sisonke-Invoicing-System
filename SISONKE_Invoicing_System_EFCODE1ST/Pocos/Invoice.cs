using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
	[Table("Invoices")]
	public class Invoice
	{
		[Key, Column("invoice_id")]
		public int InvoiceID { get; set; }

		[Column("ef_user_id"), ForeignKey("EFUser"), Required]
		public int EFUserID { get; set; }
		public virtual EFUser EFUser { get; set; }

		[Column("invoice_date", TypeName = "datetime")]
		public DateTime InvoiceDate { get; set; }

		[Column("due_date", TypeName = "datetime")]
		public DateTime DueDate { get; set; }

		[Column("subtotal")]
		public double Subtotal { get; set; }

		[Column("tax")]
		public int Tax { get; set; }

        [Column("tax_amount")]
        public double TaxAmount { get; set; }

        [Column("discount_id"), ForeignKey("Discount"), Required]
		public int DiscountID { get; set; }
		public virtual Discount Discount { get; set; }

        [Column("discount_amount")]
        public double DiscountAmount { get; set; }

        [Column("total_amount")]
		public double TotalAmount { get; set; }

		[Column("status")]
		public string Status { get; set; }

		public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
		public virtual ICollection<Note> Notes { get; set; }
		public virtual ICollection<Payment> Payments { get; set; }
	}
}