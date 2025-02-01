namespace SISONKE_Invoicing_RESTAPI.ViewModels
{
    public class PaymentVM
    {
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public double Amount { get; set; }
    }
}
