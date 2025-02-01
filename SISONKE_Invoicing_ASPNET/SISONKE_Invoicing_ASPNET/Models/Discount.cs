using System;
using System.Collections.Generic;

namespace SISONKE_Invoicing_ASPNET.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int DiscountId { get; set; }
        public string? Name { get; set; }
        public decimal Rate { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
