using System;
using System.Collections.Generic;

namespace SISONKE_Invoicing_RESTAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
