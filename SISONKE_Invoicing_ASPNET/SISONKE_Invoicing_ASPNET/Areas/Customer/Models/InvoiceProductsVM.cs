using SISONKE_Invoicing_ASPNET.Models;

namespace SISONKE_Invoicing_ASPNET.Areas.Customer.Models
{
    public class InvoiceProductsVM
    {
        public List<Product> Products { get; set; }
        public List<MyInvoices> Invoices { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
