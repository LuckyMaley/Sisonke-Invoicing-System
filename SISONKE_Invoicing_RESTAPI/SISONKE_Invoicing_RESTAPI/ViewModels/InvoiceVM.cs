namespace SISONKE_Invoicing_RESTAPI.ViewModels
{
    public class InvoiceVM
    {
        public int EfUserId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Subtotal { get; set; }
        public int Tax { get; set; }
        public int DiscountId { get; set; }
        public double TotalAmount { get; set; }
        public string? Status { get; set; }


    }
}
