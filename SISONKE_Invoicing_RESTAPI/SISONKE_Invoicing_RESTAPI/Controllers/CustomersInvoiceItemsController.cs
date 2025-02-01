using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SISONKE_Invoicing_RESTAPI.AuthModels;
using SISONKE_Invoicing_RESTAPI.DesignPatterns;
using SISONKE_Invoicing_RESTAPI.Models;
using SISONKE_Invoicing_RESTAPI.Repository;
using SISONKE_Invoicing_RESTAPI.Services;
using SISONKE_Invoicing_RESTAPI.ViewModels;

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
    /// <summary>
    /// A summary about CustomersInvoiceItemsController class.
    /// </summary>
    /// <remarks>
    /// CustomersInvoiceItemsController requires a user to be logged in and have specific role to access the end points
    /// CustomersInvoiceItemsController has the following end points:
    /// Get current logged in user's order details - required role is Customer
    /// Get All CustomersInvoiceItems information  - required role is Administrator
    /// Get a CustomersInvoiceItems with User id - Authenticated user (Administrator, Employee)
    /// Get InvoiceItems with Invoice id - Authenticated user (Administrator, Employee, Customer)
    /// Get InvoiceItems with Shipping id - Authenticated user (Administrator, Employee, Customer)
    /// Using CustomersInvoiceItemsRepo
    /// </remarks>


    [Route("invItems")]
    [ApiController]
    [Authorize]
    public class CustomersInvoiceItemsController : ControllerBase
    {
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(CustomersInvoiceItemsController));

        private readonly SISONKE_Invoicing_System_EFDBContext _context;
        private readonly IdentityHelper _identityHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomersInvoiceItemsController(SISONKE_Invoicing_System_EFDBContext context,
            UserManager<ApplicationUser> userManager,
            AuthenticationContext authContext, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _authContext = authContext;
            _roleManager = roleManager;

            _identityHelper = new IdentityHelper(userManager, authContext, roleManager);
        }

        [Route("MyInvoices")]
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetMyInvoices()
        {
            logger.Info("CustomersInvoiceItemsController - GET: invItems/MyInvoices");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            var adminRoleCheck = new AdminRoleCheckStrategy(_identityHelper);
            var employeeRoleCheck = new EmployeeRoleCheckStrategy(_identityHelper);
            bool isAdmin = await adminRoleCheck.CheckRole(userId);
            bool isEmployee = await employeeRoleCheck.CheckRole(userId);

            if (!isAdmin && !isEmployee)
            {
                try
                {
                    var loggedInUser = _context.EfUsers.FirstOrDefault(x => x.IdentityUsername == userName);
                    if (loggedInUser == null)
                    {
                        logger.Warn($"CustomersInvoiceItemsController - GET: invItems/MyInvoices - Not Found / invalid user, logged in UserName: {userName}");
                        return new NotFoundResponseFactory().CreateResponse("User not found");
                    }

                    int id = loggedInUser.EfUserId;
                    CustomersInvoiceItemsRepo repo = new CustomersInvoiceItemsRepo(_context);
                    var customersInvoices = repo.GetCustomerInvoiceItems(id).ToList();

                    return new SuccessResponseFactory().CreateResponse(customersInvoices);
                }
                catch (Exception e)
                {
                    logger.Error($"CustomersInvoiceItemsController - GET: invItems/MyInvoices - Exception: {e}");
                    return new ErrorResponseFactory().CreateResponse("Error occurred");
                }
            }

            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Get()
        {
            logger.Info("CustomersInvoiceItemsController - GET all CustomersInvoiceItems: /invItems");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            bool isAdmin = await _identityHelper.IsSuperUserRole(userId);
            bool isEmployee = await _identityHelper.IsEmployeeUserRole(userId);

            if (isAdmin || isEmployee)
            {
                CustomersInvoiceItemsRepo repo = new CustomersInvoiceItemsRepo(_context);
                var customersInvoices = repo.GetCustomerInvoiceItems(0).ToList();

                return new SuccessResponseFactory().CreateResponse(customersInvoices);
            }
            else
            {
                var user = await _userManager.FindByIdAsync(userId);
                List<string> userRoles = new List<string>(await _userManager.GetRolesAsync(user));

                logger.Error($"CustomersInvoiceItemsController - GET all CustomersInvoiceItems: /invItems - Not Authorised - logged in User: {user.UserName}");

                return new ErrorResponseFactory().CreateResponse("Not Authorised");
            }
        }

        [HttpGet("userId/{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Get(int id)
        {
            logger.Info($"CustomersInvoiceItemsController - GET: invItems/userId/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            bool isAdmin = await _identityHelper.IsSuperUserRole(userId);
            bool isEmployee = await _identityHelper.IsEmployeeUserRole(userId);

            if (!string.IsNullOrEmpty(userName) && (isAdmin || isEmployee))
            {
                CustomersInvoiceItemsRepo repo = new CustomersInvoiceItemsRepo(_context);
                var customersInvoices = repo.GetCustomerInvoiceItems(id).ToList();
                if (customersInvoices.Count > 0)
                {
                    return new SuccessResponseFactory().CreateResponse(customersInvoices);
                }
                else
                {
                    return new NotFoundResponseFactory().CreateResponse("Invoice Not Found.");
                }
            }
            else
            {
                logger.Warn($"CustomersInvoiceItemsController - GET: invItems/userId/{id} logged in User: {userName}");
                return new ErrorResponseFactory().CreateResponse("Not Authorised.");
            }
        }

        [HttpGet("InvoiceItemsInfo/{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetInvoiceInfo(int id)
        {
            logger.Info($"CustomersInvoiceItemsController - GET: invItems/InvoiceItemsInfo/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            if (!string.IsNullOrEmpty(userName))
            {
                CustomersInvoiceItemsRepo repo = new CustomersInvoiceItemsRepo(_context);
                var invoiceInfo = repo.GetInvoiceItemsByInvoiceId(id);

                return new SuccessResponseFactory().CreateResponse(invoiceInfo);
            }
            else
            {
                logger.Warn($"CustomersInvoiceItemsController - GET: invItems/InvoiceItemsInfo/{id} logged in User: {userName}");
                return new ErrorResponseFactory().CreateResponse("Not Authorised.");
            }
        }

        [HttpGet("customersByInvoicesId/{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> InvoicesDetailByInvoiceId(int id)
        {
            logger.Info($"CustomersInvoiceItemsController - GET: invItems/customersByInvoicesId/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            bool isAdmin = await _identityHelper.IsSuperUserRole(userId);
            bool isEmployee = await _identityHelper.IsEmployeeUserRole(userId);

            if (!string.IsNullOrEmpty(userName) && (isAdmin || isEmployee))
            {
                CustomersInvoiceItemsRepo repo = new CustomersInvoiceItemsRepo(_context);
                var invoicesDetail = repo.GetInvoiceItemsByInvoiceId(id).ToList();
                if (invoicesDetail.Count > 0)
                {
                    return new SuccessResponseFactory().CreateResponse(invoicesDetail);
                }
                else
                {
                    return new NotFoundResponseFactory().CreateResponse("Invoice Not Found.");
                }
            }
            else
            {
                logger.Warn($"CustomersInvoiceItemsController - GET: invItems/customersByInvoicesId/{id} logged in User: {userName}");
                return new ErrorResponseFactory().CreateResponse("Not Authorised.");
            }
        }
    }
}
