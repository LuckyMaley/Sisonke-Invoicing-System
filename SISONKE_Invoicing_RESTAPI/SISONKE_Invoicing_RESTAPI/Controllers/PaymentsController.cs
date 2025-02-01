using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.DesignPatterns;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Services;
using SISONKE_Invoicing_RESTAPI.ViewModels;
using System.Globalization;
using System.Xml.Linq;

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
    /// <summary>
    /// A summary about PaymentsController class.
    /// </summary>
    /// <remarks>
    /// PaymentsController has the following end points:
    /// Get all Payments
    /// Get Payments with id
    /// Get Payments with method
    /// Get Payments with date
    /// Get Payments between dates
    /// Put (update) Payment with id and Payment object
    /// Post (Add) Payment using a Payments View Model 
    /// Delete Payment with id
    /// </remarks>

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(PaymentsController));
        private readonly IdentityHelper _identityHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly SISONKE_Invoicing_System_EFDBContext _context;
        private readonly AuthenticationContext _authenticationContext;

        public PaymentsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SISONKE_Invoicing_System_EFDBContext context, AuthenticationContext authenticationContext)
        {
            _userManager = userManager;
            _context = context;
            _authenticationContext = authenticationContext;
            _roleManager = roleManager;
            _identityHelper = new IdentityHelper(_userManager, _authenticationContext, _roleManager);
        }

        // GET: api/Payments        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            logger.Info("PaymentsController -Get : api/Payments");
            var paymentDB = await _context.Payments.ToListAsync();

            return Ok(paymentDB);
        }

        // GET: api/Payments/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayments(int id)
        {
            logger.Info($"PaymentsController -Get : api/Payments{id}");
            List<Payment> allPayments = new List<Payment>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payments = await _context.Payments.FindAsync(id);

            if (payments == null)
            {
                logger.Warn($"PaymentsController -Get : api/Payments{id} / No Payment with that ID exists, please try again");
                return NotFound(new { message = "No Payment with that ID exists, please try again" });
            }
            else
            {
                var payOrderId = _context.Payments.FirstOrDefault(x => x.PaymentId == id).InvoiceId;
                payments.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == payOrderId);
            }

            return Ok(payments);
        }

        // GET: api/Payments/specificPayment/method
        [EnableCors("AllowOrigin")]
        [HttpGet("specificMethod/{method}")]
        public async Task<ActionResult<List<Payment>>> GetPaymentByMethod(string method)
        {
            logger.Info($"PaymentsController -Get : api/Payments{method}");
            List<Payment> payments = new List<Payment>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var paymentsQuery = _context.Payments.Where(x => x.PaymentMethod == method);
            if (paymentsQuery.Count() == 0)
            {
                logger.Warn($"PaymentsController -Get : api/Payments{method} No Payment with that Name exists, please try again");
                return NotFound(new { message = "No Payment with that method exists, please try again" });
            }
            var item = paymentsQuery;
            foreach (var paymentItem in item)
            {
                int id = paymentItem.PaymentId;


                if (paymentItem == null)
                {
                    logger.Warn($"PaymentsController -Get : api/Payments{method} No Payment with that Name exists, please try again");
                    return NotFound(new { message = "No Payment with that method exists, please try again" });
                }
                else
                {
                    var payOrderId = _context.Payments.FirstOrDefault(x => x.PaymentId == id).InvoiceId;
                    paymentItem.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == payOrderId);
                }

                payments.Add(paymentItem);
            }
            return Ok(payments);
        }

        // GET: api/Payments/SpecificDate/date
        [EnableCors("AllowOrigin")]
        [HttpGet("SpecificDateASyyyy-mm-dd/{date}")]
        public async Task<ActionResult<List<Payment>>> GetPaymentByDate(DateTime date)
        {
            logger.Info($"PaymentsController -GET : api/Payments{date}");
            List<Payment> payments = new List<Payment>();
            DateTime dateOutput;
            bool valid = DateTime.TryParse(date.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateOutput);
            if (!valid)
            {
                logger.Error($"PaymentsController -GET : api/Payments{date} / Error the format of the date is incorrect");
                return BadRequest("Error the format of the date is incorrect");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Payment> temPayments = _context.Payments.ToList();
            var paymentsQuery = temPayments.Where(x => x.PaymentDate.Date == dateOutput.Date);
            if (paymentsQuery.Count() == 0)
            {
                logger.Info($"PaymentsController -GET : api/Payments{date} / Error the format of the date is incorrect");
                return NotFound(new { message = "No Payment with that date exists, please try again" });
            }
            var item = paymentsQuery;
            foreach (var paymentItem in item)
            {
                int id = paymentItem.PaymentId;


                if (paymentItem == null)
                {
                    logger.Error($"PaymentsController -GET : api/Payments{date} / No Payment with that date exists, please try agai");
                    return NotFound(new { message = "No Payment with that date exists, please try again" });
                }
                else
                {
                    var payOrderId = _context.Payments.FirstOrDefault(x => x.PaymentId == id).InvoiceId;
                    paymentItem.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == payOrderId);
                }

                payments.Add(paymentItem);
            }
            return Ok(payments);
        }

        // GET: api/Payments/BetweenDates/date1/date2
        [EnableCors("AllowOrigin")]
        [HttpGet("BetweenDatesBothASyyyy-mm-dd/{{date1}}/{{date2}}")]
        public async Task<ActionResult<List<Payment>>> GetPaymentByBetweenDates(DateTime date1, DateTime date2)
        {
            logger.Info($"PaymentsController -POST : api/Payments");
            List<Payment> payments = new List<Payment>();
            DateTime date1Output;
            bool valid = DateTime.TryParse(date1.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out date1Output);
            if (!valid)
            {
                logger.Error($"PaymentsController -POST : api/Payments / Error the format of the date is incorrect");
                return BadRequest("Error the format of the date is incorrect");
            }

            DateTime date2Output;
            bool validDate2 = DateTime.TryParse(date2.ToShortDateString(), CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out date2Output);
            if (!validDate2)
            {
                logger.Error($"PaymentsController -POST : api/Payments / Error the format of the date is incorrect");
                return BadRequest("Error the format of the date is incorrect");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Payment> temPayments = _context.Payments.ToList();
            var paymentsQuery = temPayments.Where(x => x.PaymentDate.Date >= date1Output.Date && x.PaymentDate <= date2Output);
            if (paymentsQuery.Count() == 0)
            {
                logger.Warn($"PaymentsController -POST : api/Payments / No Payment with that date range exists, please try again");
                return NotFound(new { message = "No Payment with that date range exists, please try again" });
            }
            var item = paymentsQuery;
            foreach (var paymentItem in item)
            {
                int id = paymentItem.PaymentId;


                if (paymentItem == null)
                {
                    logger.Warn($"PaymentsController -POST : api/Payments / No Payment with that date exists, please try again");
                    return NotFound(new { message = "No Payment with that date exists, please try again" });
                }
                else
                {
                    var payOrderId = _context.Payments.FirstOrDefault(x => x.PaymentId == id).InvoiceId;
                    paymentItem.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == payOrderId);
                }

                payments.Add(paymentItem);
            }
            return Ok(payments);
        }

        // PUT: api/Payments/5
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPayments(int id, PaymentVM payment)
        {
            logger.Info($"PaymentsController -POST : api/Payments{id}");
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            if (!userSuperUserAuthorised)
            {
                logger.Warn($"PaymentsController -POST : api/Payments{id} / Not authorised to update payments");
                return BadRequest(new { message = "Not authorised to update payments" });
            }


            int currentPaymentId = 0;

            try
            {
                Payment updatePayment = _context.Payments.FirstOrDefault(o => o.PaymentId == id);
                int count = 0;
                if (updatePayment == null)
                {

                    logger.Warn($"PaymentsController -POST : api/Payments{id} /No Payment with that ID exists, please try again");
                    return NotFound(new { message = "No Payment with that ID exists, please try again" });
                }
                if (payment.PaymentMethod != "" || payment.PaymentMethod != null)
                {
                    if (updatePayment.PaymentMethod != payment.PaymentMethod)
                    {
                        updatePayment.PaymentMethod = payment.PaymentMethod;
                        count++;
                    }
                }

                if (payment.InvoiceId != 0)
                {
                    if (updatePayment.InvoiceId != payment.InvoiceId)
                    {
                        updatePayment.InvoiceId = payment.InvoiceId;
                        count++;
                    }
                }

                if (payment.Amount != 0)
                {
                    if (updatePayment.Amount != payment.Amount)
                    {
                        updatePayment.Amount = payment.Amount;
                        count++;
                    }
                }

                if (payment.PaymentMethod != "" || payment.PaymentMethod != null)
                {
                    if (updatePayment.PaymentMethod != payment.PaymentMethod)
                    {
                        updatePayment.PaymentMethod = payment.PaymentMethod;
                        count++;
                    }
                }

                if (count > 0)
                {
                    updatePayment.PaymentDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                    currentPaymentId = id;
                }
                else
                {
                    return Ok(new { message = "no updates made" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentsExists(id))
                {
                    logger.Warn($"PaymentsController -POST : api/Payments{id} /Payment Id not found, no changes made, please try again");
                    return NotFound(new { message = "Payment Id not found, no changes made, please try again" });
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

            return Ok(new { message = "Payment Updated - PaymentId:" + currentPaymentId });
        }

        // POST: api/Payments
        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Payment>> PostPayments(PaymentVM payment)
        {
            logger.Info($"PaymentsController -POST : api/Payments");
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userEmployeeAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (userSuperUserAuthorised)
            {
                logger.Warn($"PaymentsController -POST : api/Payments / Not authorised to add payments - Only Customers are allowed");
                return BadRequest(new { message = "Not authorised to add payments - Only Customers are allowed" });
            }

            if (userEmployeeAuthorised)
            {
                logger.Warn($"PaymentsController -POST : api/Payments / Not authorised to add payments - Only Customers are allowed");
                return BadRequest(new { message = "Not authorised to add payments - Only Customers are allowed" });
            }

            if (payment.InvoiceId == 0 || payment.PaymentMethod == null || payment.Amount == 0 || payment.PaymentMethod == "")
            {
                logger.Warn($"PaymentsController -POST : api/Payments / Not authorised to add payments - Only Customers are allowed");
                return BadRequest(new { message = "Cannot Add an empty payment, please you enter a valid payment" });
            }

            int currentPaymentId = 0;

            try
            {
                var newPayment = new Payment();
                newPayment.PaymentMethod = payment.PaymentMethod;
                if (_context.Invoices.Where(c => c.InvoiceId == payment.InvoiceId).Count() == 0)
                {
                    logger.Warn($"PaymentsController -POST : api/Payments / That order does not exist please choose OrderId included in the list below");
                    return BadRequest(new { message = "That order does not exist please choose OrderId included in the list below", _context.Invoices });
                }
                newPayment.InvoiceId = payment.InvoiceId;
                newPayment.Amount = payment.Amount;
                newPayment.PaymentMethod = payment.PaymentMethod;
                newPayment.PaymentDate = DateTime.Now;
                newPayment.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == payment.InvoiceId);
                _context.Payments.Add(newPayment);
                await _context.SaveChangesAsync();
                currentPaymentId = newPayment.PaymentId;
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error($"PaymentsController -POST : api/Payments / Error in adding Payment, please try again");
                return BadRequest(new { message = "Error in adding Payment, please try again" });
            }
            catch (Exception e)
            {
                logger.Error($"PaymentsController -POST : api/Payments / Error in adding Payment, please try again");
                return BadRequest(new { message = "Error in adding Payment, " + e.Message });
            }

            return Ok("New Payment Created - PaymentId:" + currentPaymentId);
        }

        // DELETE: api/Payments/5
        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Payment>> DeletePayments(int id)
        {
            logger.Info($"PaymentsController -DELETE : api/Payments");
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            if (!userSuperUserAuthorised)
            {
                logger.Warn($"PaymentsController -DELETE : api/Payments / Not authorised to delete payments");
                return BadRequest(new { message = "Not authorised to delete payments" });
            }

            var payments = await _context.Payments.FindAsync(id);
            if (payments == null)
            {
                logger.Warn($"PaymentsController -DELETE : api/Payments / Payment ID not found, please try again");
                return NotFound(new { message = "Payment ID not found, please try again" });
            }

            try
            {

                _context.Payments.Remove(payments);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error($"PaymentsController -DELETE : api/Payments / Error in deleting Payment, please try again");
                return BadRequest(new { message = "Error in deleting Payment, please try again" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error, " + e.Message });
            }
            return payments;
        }

        private bool PaymentsExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }

}
