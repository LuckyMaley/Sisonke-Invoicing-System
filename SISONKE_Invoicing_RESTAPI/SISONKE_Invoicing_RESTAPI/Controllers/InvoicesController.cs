using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.DesignPatterns;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Repository;
using SISONKE_Invoicing_RESTAPI.Services;
using SISONKE_Invoicing_RESTAPI.ViewModels;
using System.Globalization;

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
    /// <summary>
    /// A summary about InvoicesController class.
    /// </summary>
    /// <remarks>
    /// InvoicesController has the following end points:
    /// Get all Invoices
    /// Get Invoices with id
    /// Get Invoices with date
    /// Get Invoices between dates
    /// Put (update) Invoice with id and Invoice object
    /// Post (Add) Invoice using a Invoices View Model 
    /// Delete Invoice with id
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(InvoicesController));


        private readonly SISONKE_Invoicing_System_EFDBContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthenticationContext _authenticationContext;
        private readonly IdentityHelper _identityHelper;
        public InvoicesController(SISONKE_Invoicing_System_EFDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationContext authenticationContext)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationContext = authenticationContext;
            _identityHelper = new IdentityHelper(_userManager, _authenticationContext, _roleManager);
        }


        // GET: api/Invoices        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            logger.Info("CustomersPaymentsController - GET:  api/Invoices");

            var invoiceDB = await _context.Invoices.ToListAsync();

            return Ok(invoiceDB);
        }

        // GET: api/Invoices/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoices(int id)
        {
            List<Invoice> allInvoices = new List<Invoice>();

            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }

            var invoices = await _context.Invoices.FindAsync(id);

            if (invoices == null)
            {
                return NotFound(new { message = "No Invoice with that ID exists, please try again" });
            }
            else
            {
                invoices.Payments = GetAllPaymentsByInvoiceId(id);
                invoices.InvoiceItems = GetAllInvoiceItemsByInvoiceId(id);
                invoices.Notes = GetAllNotesByInvoiceId(id);
            }

            return Ok(invoices);
        }


        // GET: api/Invoices/SpecificDate/date
        [EnableCors("AllowOrigin")]
        [HttpGet("SpecificDateASyyyy-mm-dd/{date}")]
        public async Task<ActionResult<List<Invoice>>> GetInvoiceByDate(DateTime date)
        {
            List<Invoice> invoices = new List<Invoice>();
            DateTime dateOutput;
            bool valid = DateTime.TryParse(date.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateOutput);
            if (!valid)
            {
                return BadRequest("Error the format of the date is incorrect");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Invoice> temInvoices = _context.Invoices.ToList();
            var invoicesQuery = temInvoices.Where(x => x.InvoiceDate.Date == dateOutput.Date);
            if (invoicesQuery.Count() == 0)
            {
                return NotFound(new { message = "No Invoice with that date exists, please try again" });
            }
            var item = invoicesQuery;
            foreach (var invoiceItem in item)
            {
                int id = invoiceItem.InvoiceId;


                if (invoiceItem == null)
                {

                    return NotFound(new { message = "No Invoice with that date exists, please try again" });
                }
                else
                {
                    invoiceItem.Payments = GetAllPaymentsByInvoiceId(id);
                    invoiceItem.InvoiceItems = GetAllInvoiceItemsByInvoiceId(id);
                    invoiceItem.Notes = GetAllNotesByInvoiceId(id);
                }

                invoices.Add(invoiceItem);
            }
            return Ok(invoices);
        }

        // GET: api/Invoices/BetweenDates/date1/date2
        [EnableCors("AllowOrigin")]
        [HttpGet("BetweenDatesBothASyyyy-mm-dd/{{date1}}/{{date2}}")]
        public async Task<ActionResult<List<Invoice>>> GetInvoiceByBetweenDates(DateTime date1, DateTime date2)
        {
            List<Invoice> invoices = new List<Invoice>();
            DateTime date1Output;
            bool valid = DateTime.TryParse(date1.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out date1Output);
            if (!valid)
            {
                return BadRequest("Error the format of the date is incorrect");
            }

            DateTime date2Output;
            bool validDate2 = DateTime.TryParse(date2.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out date2Output);
            if (!validDate2)
            {
                return BadRequest("Error the format of the date is incorrect");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Invoice> temInvoices = _context.Invoices.ToList();
            var invoicesQuery = temInvoices.Where(x => x.InvoiceDate.Date >= date1Output.Date && x.InvoiceDate <= date2Output);
            if (invoicesQuery.Count() == 0)
            {
                return NotFound(new { message = "No Invoice with that date range exists, please try again" });
            }
            var item = invoicesQuery;
            foreach (var invoiceItem in item)
            {
                int id = invoiceItem.InvoiceId;


                if (invoiceItem == null)
                {

                    return NotFound(new { message = "No Invoice with that date exists, please try again" });
                }
                else
                {
                    invoiceItem.Payments = GetAllPaymentsByInvoiceId(id);
                    invoiceItem.InvoiceItems = GetAllInvoiceItemsByInvoiceId(id);
                    invoiceItem.Notes = GetAllNotesByInvoiceId(id);
                }

                invoices.Add(invoiceItem);
            }
            return Ok(invoices);
        }


        [Route("MyInvoice")]

        [HttpGet]
        [EnableCors("AllowOrigin")]

        public async Task<IActionResult> GetAllMyInvoices()
        {

            List<InvoiceRepoVM> customerInvoice = new List<InvoiceRepoVM>();

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string UserName = user.UserName;

            bool userAuthorisation = await _identityHelper.IsUserInRole(userId, "Customer");

            if (userAuthorisation)
            {
                try
                {
                    var customers = _context.EfUsers.Where(x => x.IdentityUsername == UserName);

                    if (customers == null)
                    {
                        logger.Warn("InvoiceController - GET: api/MyInvoice - Not Found / invalid user, logged in UserName: " + UserName);
                        return NotFound();
                    }
                    else
                    {
                        int customerId = customers.First().EfUserId;
                        CustomersInvoicesRepo prod = new CustomersInvoicesRepo(_context);
                        customerInvoice = prod.GetMyInvoices(customerId).ToList();

                        return Ok(customerInvoice);
                    }
                }
                catch (Exception e)
                {
                    // logger.Error("InvoiceController - GET:  api/MyTrades - Not Found / invalid user, logged in UserName: " + UserName + ".  Exception: " + e);

                    return BadRequest(new { message = "Not Found." });
                }


            }
            else
            {
                return BadRequest(new { message = "Not Authorised." });
            }
        }





        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutInvoices(int id, InvoiceVM invoice)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            if (!userSuperUserAuthorised)
            {
                return BadRequest(new { message = "Not authorised to update invoices" });
            }


            int currentInvoiceId = 0;

            try
            {
                Invoice updateInvoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == id);
                int count = 0;
                if (updateInvoice == null)
                {
                    return NotFound(new { message = "No Invoice with that ID exists, please try again" });
                }

                if (invoice.InvoiceDate != null)
                {
                    if (updateInvoice.InvoiceDate != invoice.InvoiceDate)
                    {
                        updateInvoice.InvoiceDate = invoice.InvoiceDate;
                        count++;
                    }
                }

                if (invoice.DueDate != null)
                {
                    if (updateInvoice.DueDate != invoice.DueDate)
                    {
                        updateInvoice.DueDate = invoice.DueDate;
                        count++;
                    }
                }


                if (invoice.Subtotal != 0)
                {
                    if (updateInvoice.Subtotal != invoice.Subtotal)
                    {
                        updateInvoice.Subtotal = invoice.Subtotal;
                        count++;
                    }
                }

                if (invoice.DiscountId != 0)
                {
                    if (updateInvoice.DiscountId != invoice.DiscountId)
                    {
                        updateInvoice.DiscountId = invoice.DiscountId;
                        count++;
                    }
                }

                if (invoice.Tax != 0)
                {
                    if (updateInvoice.Tax != invoice.Tax)
                    {
                        updateInvoice.Tax = invoice.Tax;
                        count++;
                    }
                }


                if (invoice.TotalAmount != 0)
                {
                    if (updateInvoice.TotalAmount != invoice.TotalAmount)
                    {
                        updateInvoice.TotalAmount = invoice.TotalAmount;
                        count++;
                    }
                }

                if (invoice.Status != "" || invoice.Status != null)
                {
                    if (updateInvoice.Status != invoice.Status)
                    {
                        updateInvoice.Status = invoice.Status;
                        count++;
                    }
                }





                if (count > 0)
                {
                    await _context.SaveChangesAsync();
                    currentInvoiceId = id;
                }
                else
                {
                    return Ok(new { message = "no updates made" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoicesExists(id))
                {
                    return NotFound(new { message = "Invoice Id not found, no changes made, please try again" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error, " + e.Message });
            }

            return Ok(new { message = "Invoice Updated - InvoiceId:" + currentInvoiceId });
        }

        [EnableCors("AllowOrigin")]
        [HttpPut("invPay/{id}")]
        [Authorize]
        public async Task<IActionResult> PutInvoicesForPayment(int id, InvoiceVM invoice)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);


            int currentInvoiceId = 0;

            try
            {
                Invoice updateInvoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == id);
                int count = 0;
                if (updateInvoice == null)
                {
                    return NotFound(new { message = "No Invoice with that ID exists, please try again" });
                }

                
                if (invoice.Status != "" || invoice.Status != null)
                {
                    if (updateInvoice.Status != invoice.Status)
                    {
                        updateInvoice.Status = invoice.Status;
                        count++;
                    }
                }





                if (count > 0)
                {
                    await _context.SaveChangesAsync();
                    currentInvoiceId = id;
                }
                else
                {
                    return Ok(new { message = "no updates made" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoicesExists(id))
                {
                    return NotFound(new { message = "Invoice Id not found, no changes made, please try again" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error, " + e.Message });
            }

            return Ok(new { message = "Invoice Updated - InvoiceId:" + currentInvoiceId });
        }

        // POST: api/Invoices
        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Invoice>> PostInvoices(InvoiceVM invoice)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);

            if (!userSuperUserAuthorised || !userEmployeeAuthorised)
            {
                return BadRequest(new { message = "Not authorised to add invoices" });
            }

            if (invoice.TotalAmount == 0)
            {
                return BadRequest(new { message = "Cannot Add an empty invoice, please you enter a valid invoice" });
            }

            int currentInvoiceId = 0;

            try
            {

                Invoice newInvoice = new Invoice();
                newInvoice.TotalAmount = invoice.TotalAmount;
                newInvoice.InvoiceDate = DateTime.Now;
                newInvoice.EfUserId = invoice.EfUserId;

                newInvoice.EfUser = _context.EfUsers.FirstOrDefault(c => c.EfUserId == invoice.EfUserId);



                _context.Invoices.Add(newInvoice);
                await _context.SaveChangesAsync();
                newInvoice.Payments = GetAllPaymentsByInvoiceId(newInvoice.InvoiceId);
                newInvoice.InvoiceItems = GetAllInvoiceItemsByInvoiceId(newInvoice.InvoiceId);
                newInvoice.Notes = GetAllNotesByInvoiceId(newInvoice.InvoiceId);
                _context.SaveChanges();
                currentInvoiceId = newInvoice.InvoiceId;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Error in adding Invoice, please try again" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error in adding Invoice, " + e.Message });
            }

            return Ok(new { message = "New Invoice Created - InvoiceId:" + currentInvoiceId });
        }

        
        [EnableCors("AllowOrigin")]
        [Route("InvoicesPay")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Invoice>> PostFullInvoices(FullInvoiceVM invoice)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (!userSuperUserAuthorised || !userEmployeeAuthorised)
            {
                return BadRequest(new { message = "Not authorised to add invoices" });
            }

            if (invoice.TotalAmount == 0)
            {
                return BadRequest(new { message = "Cannot Add an empty invoice, please you enter a valid invoice" });
            }

            int currentInvoiceId = 0;


            try
            {


                Invoice newInvoice = new Invoice();
                newInvoice.TotalAmount = invoice.TotalAmount;
                newInvoice.InvoiceDate = DateTime.Now;
                newInvoice.EfUserId = _context.EfUsers.FirstOrDefault(c => c.IdentityUsername == user.UserName).EfUserId;

                newInvoice.EfUser = _context.EfUsers.FirstOrDefault(c => c.IdentityUsername == user.UserName);

                _context.Invoices.Add(newInvoice);
                await _context.SaveChangesAsync();
                currentInvoiceId = newInvoice.InvoiceId;
                List<InvoiceItem> invoiceDetails = invoice.InvoiceItems.Select(od => new InvoiceItem
                {
                    InvoiceId = currentInvoiceId,
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList();

                _context.InvoiceItems.AddRange(invoiceDetails);
                Payment newPayment = new Payment();
                newPayment.InvoiceId = currentInvoiceId;
                newPayment.PaymentMethod = invoice.PaymentMethod;
                newPayment.Amount = invoice.TotalAmount;
                newPayment.PaymentDate = DateTime.Now;


                _context.Payments.Add(newPayment);
                await _context.SaveChangesAsync();
                newInvoice.Payments = GetAllPaymentsByInvoiceId(newInvoice.InvoiceId);
                newInvoice.InvoiceItems = GetAllInvoiceItemsByInvoiceId(newInvoice.InvoiceId);
                newInvoice.Notes = GetAllNotesByInvoiceId(newInvoice.InvoiceId);

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Error in adding Invoice, please try again" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error in adding Invoice, " + e.Message });
            }

            return Ok(currentInvoiceId);
        }


        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Invoice>> DeleteInvoices(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (!userSuperUserAuthorised)
            {
                return BadRequest(new { message = "Not authorised to delete invoices" });
            }

            var invoices = await _context.Invoices.FindAsync(id);
            if (invoices == null)
            {
                return NotFound(new { message = "Invoice ID not found, please try again" });
            }

            try
            {

                _context.Invoices.Remove(invoices);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Error in deleting Invoice, please try again" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error, " + e.Message });
            }
            return invoices;
        }

        private bool InvoicesExists(int id)
        {
            return _context.Invoices.Any(e => e.InvoiceId == id);
        }


        private List<InvoiceItem> GetAllInvoiceItemsByInvoiceId(int id)
        {
            List<InvoiceItem> allInvoiceItemsForInvoice = new List<InvoiceItem>();

            var invoiceDetailsQuery =
                    (from invoiceDetails in _context.InvoiceItems
                     where (invoiceDetails.InvoiceId == id)
                     select new
                     {
                         invoiceDetails.InvoiceItemId,
                         invoiceDetails.InvoiceId,
                         invoiceDetails.ProductId,
                         invoiceDetails.Quantity,
                         invoiceDetails.UnitPrice,
                         invoiceDetails.Invoice,
                         invoiceDetails.Product
                     }).ToList();


            foreach (var ordd in invoiceDetailsQuery)
            {
                allInvoiceItemsForInvoice.Add(new InvoiceItem()
                {
                    InvoiceItemId = ordd.InvoiceItemId,
                    InvoiceId = ordd.InvoiceId,
                    ProductId = ordd.ProductId,
                    Quantity = ordd.Quantity,
                    UnitPrice = ordd.UnitPrice,
                    Invoice = ordd.Invoice,
                    Product = ordd.Product
                });
            }

            return allInvoiceItemsForInvoice;
        }

        private List<Payment> GetAllPaymentsByInvoiceId(int id)
        {
            List<Payment> allPaymentsForInvoice = new List<Payment>();

            var paymentsQuery =
                    (from payments in _context.Payments
                     where (payments.InvoiceId == id)
                     select new
                     {
                         payments.PaymentId,
                         payments.InvoiceId,
                         payments.PaymentDate,
                         payments.Amount,
                         payments.PaymentMethod,

                         payments.Invoice
                     }).ToList();


            foreach (var pay in paymentsQuery)
            {
                allPaymentsForInvoice.Add(new Payment()
                {
                    PaymentId = pay.PaymentId,
                    InvoiceId = pay.InvoiceId,
                    PaymentDate = pay.PaymentDate,
                    Amount = pay.Amount,
                    PaymentMethod = pay.PaymentMethod,

                    Invoice = pay.Invoice
                });
            }

            return allPaymentsForInvoice;
        }


        private List<Note> GetAllNotesByInvoiceId(int id)
        {
            List<Note> allNotesForInvoice = new List<Note>();

            var notesQuery =
                    (from notes in _context.Notes
                     where (notes.InvoiceId == id)
                     select new
                     {
                         notes.NoteId,
                         notes.InvoiceId,
                         notes.CreatedDate,
                         notes.InvoiceNotes,
                         notes.Invoice
                     }).ToList();


            foreach (var note in notesQuery)
            {
                allNotesForInvoice.Add(new Note()
                {
                    NoteId = note.NoteId,
                    InvoiceId = note.InvoiceId,
                    CreatedDate = note.CreatedDate,
                    InvoiceNotes = note.InvoiceNotes,
                    Invoice = note.Invoice
                });
            }

            return allNotesForInvoice;
        }
    }
}
