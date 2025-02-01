namespace SISONKE_Invoicing_ASPNET.Areas.Employee.Models
{
    public class EmployeeInvoiceVM
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
