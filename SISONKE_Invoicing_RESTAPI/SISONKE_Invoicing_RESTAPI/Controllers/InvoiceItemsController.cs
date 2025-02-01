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
using SISONKE_Invoicing_RESTAPI.Services;
using SISONKE_Invoicing_RESTAPI.ViewModels;

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
    /// <summary>
    /// A summary about InvoiceItemsController class.
    /// </summary>
    /// <remarks>
    /// InvoiceItemsController has the following end points:
    /// Get all InvoiceItems
    /// Get InvoiceItems with id
    /// Put (update) InvoiceItem with id and InvoiceItem object
    /// Post (Add) InvoiceItem using a InvoiceItems View Model 
    /// Delete InvoiceItem with id
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemsController : ControllerBase
    {
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(InvoiceItemsController));
        private readonly SISONKE_Invoicing_System_EFDBContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthenticationContext _authenticationContext;
        private readonly IdentityHelper _identityHelper;
        public InvoiceItemsController(SISONKE_Invoicing_System_EFDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationContext authenticationContext)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationContext = authenticationContext;
            _identityHelper = new IdentityHelper(_userManager, _authenticationContext, _roleManager);
        }


        // GET: api/InvoiceItems        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceItem>>> GetInvoiceItems()
        {

            var invoiceItemDB = await _context.InvoiceItems.ToListAsync();

            return Ok(invoiceItemDB);
        }

        // GET: api/InvoiceItems/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceItem>> GetInvoiceItems(int id)
        {
            List<InvoiceItem> allInvoiceItems = new List<InvoiceItem>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceItems = await _context.InvoiceItems.FindAsync(id);

            if (invoiceItems == null)
            {
                return NotFound(new { message = "No InvoiceItem with that ID exists, please try again" });
            }
            else
            {
                invoiceItems.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == _context.InvoiceItems.FirstOrDefault(c => c.InvoiceItemId == id).InvoiceId);
                invoiceItems.Product = _context.Products.FirstOrDefault(c => c.ProductId == _context.InvoiceItems.FirstOrDefault(o => o.InvoiceItemId == id).ProductId);
            }

            return Ok(invoiceItems);
        }

        // PUT: api/InvoiceItems/5
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutInvoiceItems(int id, InvoiceItemsVM invoiceItem)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            if (!userSuperUserAuthorised)
            {
                return BadRequest(new { message = "Not authorised to update invoiceItems" });
            }


            int currentInvoiceItemId = 0;

            try
            {
                InvoiceItem updateInvoiceItem = _context.InvoiceItems.FirstOrDefault(o => o.InvoiceItemId == id);
                int count = 0;
                if (updateInvoiceItem == null)
                {
                    return NotFound(new { message = "No InvoiceItem with that ID exists, please try again" });
                }

                if (invoiceItem.ProductId != 0)
                {
                    if (updateInvoiceItem.ProductId != invoiceItem.ProductId)
                    {
                        updateInvoiceItem.ProductId = invoiceItem.ProductId;
                        updateInvoiceItem.Product = _context.Products.FirstOrDefault(c => c.ProductId == invoiceItem.ProductId);
                        count++;
                    }
                }

                if (invoiceItem.InvoiceId != 0)
                {
                    if (updateInvoiceItem.InvoiceId != invoiceItem.InvoiceId)
                    {
                        updateInvoiceItem.InvoiceId = invoiceItem.InvoiceId;
                        updateInvoiceItem.Invoice = _context.Invoices.FirstOrDefault(c => c.InvoiceId == invoiceItem.InvoiceId);
                        count++;
                    }
                }

                if (invoiceItem.Quantity != 0)
                {
                    if (updateInvoiceItem.Quantity != invoiceItem.Quantity)
                    {
                        updateInvoiceItem.Quantity = invoiceItem.Quantity;
                        count++;
                    }
                }

                if (invoiceItem.UnitPrice != 0)
                {
                    if (updateInvoiceItem.UnitPrice != invoiceItem.UnitPrice)
                    {
                        updateInvoiceItem.UnitPrice = invoiceItem.UnitPrice;
                        count++;
                    }
                }

                if (count > 0)
                {
                    await _context.SaveChangesAsync();
                    currentInvoiceItemId = id;
                }
                else
                {
                    return Ok(new { message = "no updates made" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceItemsExists(id))
                {
                    return NotFound(new { message = "InvoiceItem Id not found, no changes made, please try again" });
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

            return Ok(new { message = "InvoiceItem Updated - InvoiceItemId:" + currentInvoiceItemId });
        }

        // POST: api/InvoiceItems
        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<InvoiceItem>> PostInvoiceItems(InvoiceItemsVM invoiceItem)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userSellerAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (userSuperUserAuthorised)
            {

                return BadRequest(new { message = "Not authorised to add invoiceItems - Only Customers are allowed" });
            }

            if (userSellerAuthorised)
            {
                return BadRequest(new { message = "Not authorised to add invoiceItems - Only Customers are allowed" });
            }

            if (invoiceItem.InvoiceId == 0 || invoiceItem.UnitPrice == 0 || invoiceItem.Quantity == 0 || invoiceItem.ProductId == 0)
            {
                return BadRequest(new { message = "Cannot Add an empty order detail, please you enter a valid order detail" });
            }

            int currentInvoiceItemId = 0;

            try
            {
                InvoiceItem newInvoiceItem = new InvoiceItem();
                if (_context.Products.Where(c => c.ProductId == invoiceItem.ProductId).Count() == 0)
                {
                    return BadRequest(new { message = "That product does not exist please choose ProductId included in the list below", _context.Products });
                }
                newInvoiceItem.ProductId = invoiceItem.ProductId;
                if (_context.Invoices.Where(c => c.InvoiceId == invoiceItem.InvoiceId).Count() == 0)
                {
                    return BadRequest(new { message = "That order does not exist please choose InvoiceId included in the list below", _context.Invoices });
                }
                newInvoiceItem.InvoiceId = invoiceItem.InvoiceId;
                newInvoiceItem.Quantity = invoiceItem.Quantity;
                newInvoiceItem.UnitPrice = invoiceItem.UnitPrice;
                newInvoiceItem.Invoice = _context.Invoices.FirstOrDefault(o => o.InvoiceId == invoiceItem.InvoiceId);
                newInvoiceItem.Product = _context.Products.FirstOrDefault(c => c.ProductId == invoiceItem.ProductId);


                _context.InvoiceItems.Add(newInvoiceItem);
                await _context.SaveChangesAsync();
                currentInvoiceItemId = _context.InvoiceItems.Max(c => c.InvoiceItemId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Error in adding InvoiceItem, please try again" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error in adding InvoiceItem, " + e.Message });
            }

            return Ok("New InvoiceItem Created - InvoiceItemId:" + currentInvoiceItemId);
        }

        // DELETE: api/InvoiceItems
        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<InvoiceItem>> DeleteInvoiceItems(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool userSuperUserAuthorised = await _identityHelper.IsSuperUserRole(userId);
            bool userSellerAuthorised = await _identityHelper.IsEmployeeUserRole(userId);
            if (!userSuperUserAuthorised)
            {
                return BadRequest(new { message = "Not authorised to delete invoiceItems" });
            }

            var invoiceItems = await _context.InvoiceItems.FindAsync(id);
            if (invoiceItems == null)
            {
                return NotFound(new { message = "InvoiceItem ID not found, please try again" });
            }

            try
            {

                _context.InvoiceItems.Remove(invoiceItems);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Error in deleting InvoiceItem, please try again" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error, " + e.Message });
            }
            return invoiceItems;
        }

        private bool InvoiceItemsExists(int id)
        {
            return _context.InvoiceItems.Any(e => e.InvoiceItemId == id);
        }
    }
}

