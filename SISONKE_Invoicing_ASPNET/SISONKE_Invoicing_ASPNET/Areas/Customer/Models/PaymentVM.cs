namespace SISONKE_Invoicing_ASPNET.Areas.Customer.Models
{
    public class PaymentVM
    {
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public double Amount { get; set; }
    }
}
