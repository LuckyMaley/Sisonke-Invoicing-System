using System;
using System.Collections.Generic;

namespace SISONKE_Invoicing_ASPNET.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
            Notes = new HashSet<Note>();
            Payments = new HashSet<Payment>();
        }

        public int InvoiceId { get; set; }
        public int EfUserId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Subtotal { get; set; }
        public int Tax { get; set; }
        public double TaxAmount { get; set; }
        public int DiscountId { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalAmount { get; set; }
        public string? Status { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual EfUser EfUser { get; set; } = null!;
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
