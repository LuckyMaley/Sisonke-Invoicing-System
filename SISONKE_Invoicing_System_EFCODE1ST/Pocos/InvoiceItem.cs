using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
    [Table("invoice_items")]
    public class InvoiceItem
    {
        [Key, Column("invoice_item_id")]
        public int InvoiceItemID { get; set; }

        [ForeignKey("Invoice"), Required, Column("invoice_id")]
        public int InvoiceID { get; set; }
		public virtual Invoice Invoice { get; set; }

		[ForeignKey("Product"), Required, Column("product_id")]
        public int ProductID { get; set; }
		public virtual Product Product { get; set; }

		[Column("quantity")]
        public int Quantity { get; set; }

        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }
    }
}
