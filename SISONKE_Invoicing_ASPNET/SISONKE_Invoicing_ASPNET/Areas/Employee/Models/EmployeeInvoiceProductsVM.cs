using SISONKE_Invoicing_ASPNET.Areas.Customer.Models;
using SISONKE_Invoicing_ASPNET.Models;

namespace SISONKE_Invoicing_ASPNET.Areas.Employee.Models
{
    public class EmployeeInvoiceProductsVM
    {
        public List<Product> Products { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
