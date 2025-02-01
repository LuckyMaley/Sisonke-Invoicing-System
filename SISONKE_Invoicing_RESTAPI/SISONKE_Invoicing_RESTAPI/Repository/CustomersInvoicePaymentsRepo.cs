using Microsoft.VisualBasic;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.ViewModels;
using System.Xml.Linq;

namespace SISONKE_Invoicing_RESTAPI.Repository
{

    /// <summary>
    /// A summary about CustomersInvoicePaymentsRepo class.
    /// </summary>
    /// <remarks>
    /// CustomersInvoicePaymentsRepo has the following methods:
    /// Get current logged in user's invoice payments
    /// Get invoice with payment id
    /// Get invoice with invoice id
    /// </remarks>
    public class CustomersInvoicePaymentsRepo
    {
        private readonly SISONKE_Invoicing_System_EFDBContext _context;

        public CustomersInvoicePaymentsRepo(SISONKE_Invoicing_System_EFDBContext context)
        {
            _context = context;
        }

        public virtual List<CustomersPaymentsVM> GetInvoicePayments(int userId)
        {
            int paramId = userId;
            List<CustomersPaymentsVM> invoicesPayments = new List<CustomersPaymentsVM>();

            var invoicesPaymentsQuery =
                (from efUsers in _context.EfUsers
                 join invoices in _context.Invoices
                 on efUsers.EfUserId equals invoices.EfUserId
                 join discounts in _context.Discounts
                 on invoices.DiscountId equals discounts.DiscountId
                 join payments in _context.Payments
                 on invoices.InvoiceId equals payments.InvoiceId
                 where ((paramId == 0 && efUsers.EfUserId == efUsers.EfUserId) || (efUsers.EfUserId == paramId))
             
                 select new
                 {
                     EfUserId = efUsers.EfUserId,
                     FirstName = efUsers.FirstName,
                     LastName = efUsers.LastName,
                     Email = efUsers.Email,
                     Address = efUsers.Address,
                     PhoneNumber = efUsers.PhoneNumber,
                     IdentityUsername = efUsers.IdentityUsername,
                     Role = efUsers.Role,
                     InvoiceId = invoices.InvoiceId,
                     DiscountId = discounts.DiscountId,
                     InvoiceDate = invoices.InvoiceDate,
                     DueDate = invoices.DueDate,
                     Subtotal = invoices.Subtotal,
                     Tax = invoices.Tax,
                     TotalAmount = invoices.TotalAmount,
                     Status = invoices.Status,
                     Name = discounts.Name,
                     Rate = discounts.Rate,
                    PaymentId = payments.PaymentId,
                    PaymentDate = payments.PaymentDate,
                    PaymentMethod = payments.PaymentMethod,

    }).ToList();

            foreach (var cust in invoicesPaymentsQuery)
            {
                invoicesPayments.Add(new CustomersPaymentsVM()
                {

                    EfUserId = cust.EfUserId,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Address = cust.Address,
                    PhoneNumber = cust.PhoneNumber,
                    IdentityUsername = cust.IdentityUsername,
                    InvoiceId = cust.InvoiceId,
                    InvoiceDate = cust.InvoiceDate,
                    DueDate = cust.DueDate,
                    Subtotal = cust.Subtotal,
                    Tax = cust.Tax,
                    DiscountId = cust.DiscountId,
                    Status = cust.Status,
                    Name = cust.Name,
                    Rate = cust.Rate,
                    TotalAmount = cust.TotalAmount,
                    PaymentId = cust.PaymentId,
                    PaymentDate = cust.PaymentDate,
                    PaymentMethod = cust.PaymentMethod,

                });
            }

            return invoicesPayments;
        }

        public virtual CustomersPaymentsVM GetPaymentDetails(int paymentId)
        {
            int paramId = paymentId;
            CustomersPaymentsVM invoicesPayments = new CustomersPaymentsVM();

            var invoicesPaymentsQuery =
               (from efUsers in _context.EfUsers
                join invoices in _context.Invoices
                 on efUsers.EfUserId equals invoices.EfUserId
                join discounts in _context.Discounts
                on invoices.DiscountId equals discounts.DiscountId
                join payments in _context.Payments
                on invoices.InvoiceId equals payments.InvoiceId
                where (payments.PaymentId == paramId)
        
                select new
                {
                    EfUserId = efUsers.EfUserId,
                    FirstName = efUsers.FirstName,
                    LastName = efUsers.LastName,
                    Email = efUsers.Email,
                    Address = efUsers.Address,
                    PhoneNumber = efUsers.PhoneNumber,
                    IdentityUsername = efUsers.IdentityUsername,
                    Role = efUsers.Role,
                    InvoiceId = invoices.InvoiceId,
                    DiscountId = discounts.DiscountId,
                    InvoiceDate = invoices.InvoiceDate,
                    DueDate = invoices.DueDate,
                    Subtotal = invoices.Subtotal,
                    Tax = invoices.Tax,
                    TotalAmount = invoices.TotalAmount,
                    Status = invoices.Status,
                    Name = discounts.Name,
                    Rate = discounts.Rate,
                    PaymentId = payments.PaymentId,
                    PaymentDate = payments.PaymentDate,
                    PaymentMethod = payments.PaymentMethod,
                }).ToList();

            foreach (var cust in invoicesPaymentsQuery)
            {
                invoicesPayments.EfUserId = cust.EfUserId;
                invoicesPayments.FirstName = cust.FirstName;
                invoicesPayments.LastName = cust.LastName;
                invoicesPayments.Email = cust.Email;
                invoicesPayments.Address = cust.Address;
                invoicesPayments.PhoneNumber = cust.PhoneNumber;
                invoicesPayments.IdentityUsername = cust.IdentityUsername;

                invoicesPayments.InvoiceId = cust.InvoiceId;
                     invoicesPayments.InvoiceDate = cust.InvoiceDate;
                     invoicesPayments.DueDate = cust.DueDate;
                     invoicesPayments.Subtotal = cust.Subtotal;
                     invoicesPayments.Tax = cust.Tax;
                     invoicesPayments.DiscountId = cust.DiscountId;
                     invoicesPayments.Status = cust.Status;
                     invoicesPayments.Name = cust.Name;
                     invoicesPayments.Rate = cust.Rate;
                     invoicesPayments.TotalAmount = cust.TotalAmount;
                     invoicesPayments.PaymentId = cust.PaymentId;
                     invoicesPayments.PaymentDate = cust.PaymentDate;
                     invoicesPayments.PaymentMethod = cust.PaymentMethod;

            }

            return invoicesPayments;
        }

        public virtual List<CustomersPaymentsVM> GetPaymentsByInvoiceId(int invoiceId)
        {
            int paramId = invoiceId;
            List<CustomersPaymentsVM> invoicesPayments = new List<CustomersPaymentsVM>();

            var invoicesPaymentsQuery =
                (from efUsers in _context.EfUsers
                 join invoices in _context.Invoices
               on efUsers.EfUserId equals invoices.EfUserId
                 join discounts in _context.Discounts
                 on invoices.DiscountId equals discounts.DiscountId
                 join payments in _context.Payments
                 on invoices.InvoiceId equals payments.InvoiceId
                 where (payments.PaymentId == paramId)

                 //invoiceby invoices.OrderDate, invoices.TotalAmount descending
                 select new
                 {
                     EfUserId = efUsers.EfUserId,
                     FirstName = efUsers.FirstName,
                     LastName = efUsers.LastName,
                     Email = efUsers.Email,
                     Address = efUsers.Address,
                     PhoneNumber = efUsers.PhoneNumber,
                     IdentityUsername = efUsers.IdentityUsername,
                     Role = efUsers.Role,
                     InvoiceId = invoices.InvoiceId,
                     DiscountId = discounts.DiscountId,
                     InvoiceDate = invoices.InvoiceDate,
                     DueDate = invoices.DueDate,
                     Subtotal = invoices.Subtotal,
                     Tax = invoices.Tax,
                     TotalAmount = invoices.TotalAmount,
                     Status = invoices.Status,
                     Name = discounts.Name,
                     Rate = discounts.Rate,
                     PaymentId = payments.PaymentId,
                     PaymentDate = payments.PaymentDate,
                     PaymentMethod = payments.PaymentMethod,
                 }).ToList();

            foreach (var cust in invoicesPaymentsQuery)
            {
                invoicesPayments.Add(new CustomersPaymentsVM()
                {
                    EfUserId = cust.EfUserId,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Address = cust.Address,
                    PhoneNumber = cust.PhoneNumber,
                    IdentityUsername = cust.IdentityUsername,
                    InvoiceId = cust.InvoiceId,
                    InvoiceDate = cust.InvoiceDate,
                    DueDate = cust.DueDate,
                    Subtotal = cust.Subtotal,
                    Tax = cust.Tax,
                    DiscountId = cust.DiscountId,
                    Status = cust.Status,
                    Name = cust.Name,
                    Rate = cust.Rate,
                    TotalAmount = cust.TotalAmount,
                    PaymentId = cust.PaymentId,
                    PaymentDate = cust.PaymentDate,
                    PaymentMethod = cust.PaymentMethod,


                });
            }

            return invoicesPayments;
        }
    }
}
