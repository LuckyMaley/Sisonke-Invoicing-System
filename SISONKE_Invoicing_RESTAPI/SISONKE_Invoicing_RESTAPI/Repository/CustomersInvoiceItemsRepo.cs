using Microsoft.CodeAnalysis;
using Microsoft.VisualBasic;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.ViewModels;
using System.Net;
using System.Xml.Linq;

namespace SISONKE_Invoicing_RESTAPI.Repository
{
    /// <summary>
    /// A summary about CustomersInvoiceItemsRepo class.
    /// </summary>
    /// <remarks>
    /// CustomersInvoiceItemsRepo has the following methods:
    /// Get current logged in user's products
    /// Get invoiceItems with order details id
    /// Get invoiceItems with order id
    /// </remarks>
    public class CustomersInvoiceItemsRepo
    {
        private readonly SISONKE_Invoicing_System_EFDBContext _context;

        public CustomersInvoiceItemsRepo(SISONKE_Invoicing_System_EFDBContext context)
        {
            _context = context;
        }

        public virtual List<CustomersInvoiceItemsVM> GetCustomerInvoiceItems(int userId)
        {
            int paramId = userId;
            List<CustomersInvoiceItemsVM> customersInvoiceItems = new List<CustomersInvoiceItemsVM>();

            var customersInvoiceItemsQuery =
                (from efUsers in _context.EfUsers
                 join invoices in _context.Invoices
                 on efUsers.EfUserId equals invoices.EfUserId
                 join discounts in _context.Discounts
                 on invoices.DiscountId equals discounts.DiscountId
                 join invoiceItems in _context.InvoiceItems
                 on invoices.InvoiceId equals invoiceItems.InvoiceId
                 join products in _context.Products
                 on invoiceItems.ProductId equals products.ProductId
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
                     InvoiceItemId = invoiceItems.InvoiceItemId,
                     Quantity = invoiceItems.Quantity,
                     UnitPrice = invoiceItems.UnitPrice,
                     TotalPrice = invoiceItems.TotalPrice,
                     ProductId = products.ProductId,
                     ProductName = products.Name,
                     Description = products.Description,
                     Price = products.Price,
                     StockQuantity = products.StockQuantity,
                     ModifiedDate = products.ModifiedDate
    }).ToList();

            foreach (var cust in customersInvoiceItemsQuery)
            {
                customersInvoiceItems.Add(new CustomersInvoiceItemsVM()
                {
                    EfUserId = cust.EfUserId,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Address = cust.Address,
                    PhoneNumber = cust.PhoneNumber,
                    IdentityUsername = cust.IdentityUsername,
                    InvoiceId = cust.InvoiceId,
                    DiscountId = cust.DiscountId,
                    InvoiceDate = cust.InvoiceDate,
                    DueDate = cust.DueDate,
                    Subtotal = cust.Subtotal,
                    Tax = cust.Tax,
                    TotalAmount = cust.TotalAmount,
                    Status = cust.Status,
                    Name = cust.Name,
                    Rate = cust.Rate,
                    InvoiceItemId = cust.InvoiceItemId,
                    Quantity = cust.Quantity,
                    UnitPrice = cust.UnitPrice,
                    TotalPrice = cust.TotalPrice,
                    ProductId = cust.ProductId,
                    ProductName = cust.Name,
                    Description = cust.Description,
                    Price = cust.Price,
                    StockQuantity = cust.StockQuantity,
                    ModifiedDate = cust.ModifiedDate
                });
            }

            return customersInvoiceItems;
        }



        public virtual CustomersInvoiceItemsVM GetInvoiceItems(int invoiceItemsId)
        {
            int paramId = invoiceItemsId;
            CustomersInvoiceItemsVM customersInvoiceItems = new CustomersInvoiceItemsVM();

            var customersInvoiceItemsQuery =
                (from efUsers in _context.EfUsers
                 join invoices in _context.Invoices
                 on efUsers.EfUserId equals invoices.EfUserId
                 join discounts in _context.Discounts
                 on invoices.DiscountId equals discounts.DiscountId
                 join invoiceItems in _context.InvoiceItems
                 on invoices.InvoiceId equals invoiceItems.InvoiceId
                 join products in _context.Products
                 on invoiceItems.ProductId equals products.ProductId
                 where (invoiceItems.InvoiceItemId == paramId)
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
                     InvoiceItemId = invoiceItems.InvoiceItemId,
                     Quantity = invoiceItems.Quantity,
                     UnitPrice = invoiceItems.UnitPrice,
                     TotalPrice = invoiceItems.TotalPrice,
                     ProductId = products.ProductId,
                     ProductName = products.Name,
                     Description = products.Description,
                     Price = products.Price,
                     StockQuantity = products.StockQuantity,
                     ModifiedDate = products.ModifiedDate
                 }).ToList();

            foreach (var cust in customersInvoiceItemsQuery)
            {
                customersInvoiceItems.EfUserId = cust.EfUserId;
                customersInvoiceItems.FirstName = cust.FirstName;
                customersInvoiceItems.LastName = cust.LastName;
                customersInvoiceItems.Email = cust.Email;
                customersInvoiceItems.Address = cust.Address;
                customersInvoiceItems.PhoneNumber = cust.PhoneNumber;
                customersInvoiceItems.IdentityUsername = cust.IdentityUsername;
                customersInvoiceItems.InvoiceId = cust.InvoiceId;
                customersInvoiceItems.DiscountId = cust.DiscountId;
                customersInvoiceItems.InvoiceDate = cust.InvoiceDate;
                customersInvoiceItems.DueDate = cust.DueDate;
                customersInvoiceItems.Subtotal = cust.Subtotal;
                customersInvoiceItems.Tax = cust.Tax;
                customersInvoiceItems.TotalAmount = cust.TotalAmount;
                customersInvoiceItems.Status = cust.Status;
                customersInvoiceItems.Name = cust.Name;
                customersInvoiceItems.Rate = cust.Rate;
                customersInvoiceItems.InvoiceItemId = cust.InvoiceItemId;
                customersInvoiceItems.Quantity = cust.Quantity;
                customersInvoiceItems.UnitPrice = cust.UnitPrice;
                customersInvoiceItems.TotalPrice = cust.TotalPrice;
                customersInvoiceItems.ProductId = cust.ProductId;
                customersInvoiceItems.ProductName = cust.Name;
                customersInvoiceItems.Description = cust.Description;
                customersInvoiceItems.Price = cust.Price;
                customersInvoiceItems.StockQuantity = cust.StockQuantity;
                customersInvoiceItems.ModifiedDate = cust.ModifiedDate;
            }

            return customersInvoiceItems;
        }


        public virtual List<CustomersInvoiceItemsVM> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            int paramId = invoiceId;
            List<CustomersInvoiceItemsVM> customersInvoiceItems = new List<CustomersInvoiceItemsVM>();

            var customersInvoiceItemsQuery =
                (from efUsers in _context.EfUsers
                 join invoices in _context.Invoices
                 on efUsers.EfUserId equals invoices.EfUserId
                 join discounts in _context.Discounts
                 on invoices.DiscountId equals discounts.DiscountId
                 join invoiceItems in _context.InvoiceItems
                 on invoices.InvoiceId equals invoiceItems.InvoiceId
                 join products in _context.Products
                 on invoiceItems.ProductId equals products.ProductId
                 where (invoices.InvoiceId == paramId)
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
                     InvoiceItemId = invoiceItems.InvoiceItemId,
                     Quantity = invoiceItems.Quantity,
                     UnitPrice = invoiceItems.UnitPrice,
                     TotalPrice = invoiceItems.TotalPrice,
                     ProductId = products.ProductId,
                     ProductName = products.Name,
                     Description = products.Description,
                     Price = products.Price,
                     StockQuantity = products.StockQuantity,
                     ModifiedDate = products.ModifiedDate
                 }).ToList();

            foreach (var cust in customersInvoiceItemsQuery)
            {
                customersInvoiceItems.Add(new CustomersInvoiceItemsVM()
                {
                    EfUserId = cust.EfUserId,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email = cust.Email,
                    Address = cust.Address,
                    PhoneNumber = cust.PhoneNumber,
                    IdentityUsername = cust.IdentityUsername,
                    InvoiceId = cust.InvoiceId,
                    DiscountId = cust.DiscountId,
                    InvoiceDate = cust.InvoiceDate,
                    DueDate = cust.DueDate,
                    Subtotal = cust.Subtotal,
                    Tax = cust.Tax,
                    TotalAmount = cust.TotalAmount,
                    Status = cust.Status,
                    Name = cust.Name,
                    Rate = cust.Rate,
                    InvoiceItemId = cust.InvoiceItemId,
                    Quantity = cust.Quantity,
                    UnitPrice = cust.UnitPrice,
                    TotalPrice = cust.TotalPrice,
                    ProductId = cust.ProductId,
                    ProductName = cust.Name,
                    Description = cust.Description,
                    Price = cust.Price,
                    StockQuantity = cust.StockQuantity,
                    ModifiedDate = cust.ModifiedDate
                });
            }

            return customersInvoiceItems;
        }
    }
}
