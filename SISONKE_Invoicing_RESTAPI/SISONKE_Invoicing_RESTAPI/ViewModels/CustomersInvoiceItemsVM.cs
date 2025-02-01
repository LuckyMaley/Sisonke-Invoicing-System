namespace SISONKE_Invoicing_RESTAPI.ViewModels
{
    public class CustomersInvoiceItemsVM
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

        public int InvoiceItemId { get; set; }        
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
