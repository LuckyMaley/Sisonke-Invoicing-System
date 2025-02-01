using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
	[Table("payments")]
	public class Payment
	{
		[Key, Column("payment_id")]
		public int PaymentID { get; set; }

		[ForeignKey("Invoice"), Required, Column("invoice_id")]
		public int InvoiceID { get; set; }
		public virtual Invoice Invoice { get; set; }

		[Column("payment_date", TypeName = "datetime")]
		public DateTime PaymentDate { get; set; }

		[StringLength(100), Column("payment_method")]
		public string PaymentMethod { get; set; }

		[Column("amount")]
		public double Amount { get; set; }

       

    }
}