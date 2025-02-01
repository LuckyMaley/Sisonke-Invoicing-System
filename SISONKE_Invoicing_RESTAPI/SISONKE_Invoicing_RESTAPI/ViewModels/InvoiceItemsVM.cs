namespace SISONKE_Invoicing_RESTAPI.ViewModels
{
    //updated
    public class InvoiceItemsVM
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
