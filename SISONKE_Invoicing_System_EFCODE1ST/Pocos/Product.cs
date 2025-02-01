using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos
{
	[Table("products")]
	public class Product
	{

		[Key, Column("product_id")]
		public int ProductID { get; set; }

		[StringLength(100), Column("name")]
		public string Name { get; set; }

		[StringLength(300), Column("description")]
		public string Description { get; set; }

		[Column("price")]
		public decimal Price { get; set; }

		[Column("stock_quantity")]
		public int StockQuantity { get; set; }

		[Column("modified_date", TypeName = "datetime")]
		public DateTime ModifiedDate { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
