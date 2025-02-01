using SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Models;
using SISONKE_Invoicing_ASPNET.Models;

namespace SISONKE_Invoicing_ASPNET.Areas.Administrator.Models
{
    public class AdminInvoiceProductVM
    {
        public List<Product> Products { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public List<UserProfileVM> Users { get; set; }

    }
}
