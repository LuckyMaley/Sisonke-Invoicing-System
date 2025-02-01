namespace SISONKE_Invoicing_ASPNET.Areas.Customer.Models
{
    public class MyInvoices
    {
        public int EfUserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string IdentityUsername { get; set; } = null!;

        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Subtotal { get; set; }
        public int Tax { get; set; }
        public int DiscountId { get; set; }
        public double TotalAmount { get; set; }
        public string? Status { get; set; }

        public string? Name { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
