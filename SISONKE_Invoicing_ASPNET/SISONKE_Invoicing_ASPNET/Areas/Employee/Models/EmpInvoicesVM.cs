using SISONKE_Invoicing_ASPNET.Models;

namespace SISONKE_Invoicing_ASPNET.Areas.Employee.Models
{
    public class EmpInvoicesVM
    {
        public List<CustomersInvoiceItemsVM> CustomersInvoiceItemsVM { get; set; }
        public List<Product> Products { get; set; }
    }
}
