using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.ViewModels;
using System.Linq;

namespace SISONKE_Invoicing_RESTAPI.Repository
{

    /// <summary>
    /// A summary about CustomersInvoicesRepo class.
    /// </summary>
    /// <remarks>
    /// CustomersInvoicesRepo has the following methods:
    /// Get current logged in user's invoices
    /// Get invoices with invoice id
    /// </remarks>
    public class CustomersInvoicesRepo
    {
        private readonly SISONKE_Invoicing_System_EFDBContext _context;

        public CustomersInvoicesRepo(SISONKE_Invoicing_System_EFDBContext context)

        {

            _context = context;

        }

        public virtual List<InvoiceRepoVM> GetMyInvoices(int userId)

        {

            int paramId = userId;

            List<InvoiceRepoVM> customersInvoices = new List<InvoiceRepoVM>();

            var customersInvoicesQuery =

                (from efUsers in _context.EfUsers
                 join invoices in _context.Invoices
                 on efUsers.EfUserId equals invoices.EfUserId
                 join discounts in _context.Discounts
                 on invoices.DiscountId equals discounts.DiscountId
                 where ((paramId == 0 && efUsers.EfUserId == efUsers.EfUserId) || (efUsers.EfUserId == paramId))
                 orderby invoices.EfUserId, invoices.InvoiceDate
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
                 }).ToList();

            foreach (var cust in customersInvoicesQuery)

            {

                customersInvoices.Add(new InvoiceRepoVM()
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
                    TotalAmount = cust.TotalAmount
                });

            }

            return customersInvoices;

        }

        public virtual InvoiceRepoVM GetInvoiceDetails(int invoiceId)
        {
            int paramId = invoiceId;
            InvoiceRepoVM customersInvoices = new InvoiceRepoVM();

            var customersInvoicesQuery =
               (from efUsers in _context.EfUsers
                join invoices in _context.Invoices
                on efUsers.EfUserId equals invoices.EfUserId
                join discounts in _context.Discounts
                on invoices.DiscountId equals discounts.DiscountId
                where (invoices.InvoiceId == paramId)
                orderby invoices.EfUserId, invoices.InvoiceDate
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
                }).ToList();

            foreach (var cust in customersInvoicesQuery)
            {
                customersInvoices.EfUserId = cust.EfUserId;
                customersInvoices.FirstName = cust.FirstName;
                customersInvoices.LastName = cust.LastName;
                customersInvoices.Email = cust.Email;
                customersInvoices.Address = cust.Address;
                customersInvoices.PhoneNumber = cust.PhoneNumber;
                customersInvoices.IdentityUsername = cust.IdentityUsername;
                customersInvoices.InvoiceId = cust.InvoiceId;
                customersInvoices.InvoiceDate = cust.InvoiceDate;
                customersInvoices.DueDate = cust.DueDate;
                customersInvoices.Subtotal = cust.Subtotal;
                customersInvoices.Tax = cust.Tax;
                customersInvoices.DiscountId = cust.DiscountId;
                customersInvoices.Status = cust.Status;
                customersInvoices.Name = cust.Name;
                customersInvoices.Rate = cust.Rate;
                customersInvoices.TotalAmount = cust.TotalAmount;
            }

            return customersInvoices;
        }


        public virtual List<InvoiceRepoVM> GetInvoicesByDiscountId(int discountId)
        {
            int paramId = discountId;
            List<InvoiceRepoVM> customersInvoices = new List<InvoiceRepoVM>();

            var customersInvoicesQuery =
                (from efUsers in _context.EfUsers
                 join invoices in _context.Invoices
                 on efUsers.EfUserId equals invoices.EfUserId
                 join discounts in _context.Discounts
                 on invoices.DiscountId equals discounts.DiscountId
                 where (invoices.DiscountId == paramId)
                 orderby invoices.InvoiceDate, invoices.TotalAmount descending
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
                 }).ToList();

            foreach (var cust in customersInvoicesQuery)
            {
                customersInvoices.Add(new InvoiceRepoVM()
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
                    TotalAmount = cust.TotalAmount
                });
            }

            return customersInvoices;
        }

    }
}

