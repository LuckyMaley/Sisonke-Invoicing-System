namespace SISONKE_Invoicing_RESTAPI.ViewModels
{
    public class FullInvoiceVM
    {
         public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Subtotal { get; set; }
        public int Tax { get; set; }
        public int DiscountId { get; set; }
        public double TotalAmount { get; set; }
        public string? Status { get; set; }

        public string? PaymentMethod { get; set; }
       
        public List<InvoiceItemsVM> InvoiceItems { get; set; }

    }
}
