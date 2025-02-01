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

namespace SISONKE_Invoicing_RESTAPI.Controllers
{
    /// <summary>
    /// A summary about CustomersCustomersPaymentsController class.
    /// </summary>
    /// <remarks>
    /// CustomersPaymentsController requires a user to be logged in and have specific role to access the end points.
    /// CustomersPaymentsController has the following end points:
    /// Get current logged in user's invoice details - required role is Customer.
    /// Get All CustomersPayments information  - required role is (Administrator, Employee).
    /// Get a CustomersPayments with User id - Authenticated user (Administrator, Employee).
    /// Using CustomersPaymentsRepo.
    /// </remarks>

    [Route("cusinvPay")]
    [ApiController]
    [Authorize]
    public class CustomersPaymentsController : ControllerBase
    {
        private static readonly ILog logger = LogManagerSingleton.Instance.GetLogger(nameof(CustomersPaymentsController));

        private readonly SISONKE_Invoicing_System_EFDBContext _context;
        private readonly IdentityHelper _identityHelper;

        private UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationContext _authContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomersPaymentsController(SISONKE_Invoicing_System_EFDBContext context,
            UserManager<ApplicationUser> userManager,
            AuthenticationContext authContext, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _authContext = authContext;
            _roleManager = roleManager;

            _identityHelper = new IdentityHelper(userManager, authContext, roleManager);
        }

        [Route("MyPayments")]
        // GET: /cusinvPay/MyPayments
        [HttpGet]
        [EnableCors("AllowOrigin")]
        // [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyPayments()
        {
            logger.Info("CustomersPaymentsController - GET:  cusinvPay/MyPayments");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            var adminRoleCheck = new AdminRoleCheckStrategy(_identityHelper);
            var employeeRoleCheck = new EmployeeRoleCheckStrategy(_identityHelper);
            bool isAdmin = await adminRoleCheck.CheckRole(userId);
            bool isEmployee = await employeeRoleCheck.CheckRole(userId);

            if (!isAdmin && !isEmployee)
            {
                var loggedInUser = _context.EfUsers.FirstOrDefault(x => x.IdentityUsername == userName);
                if (loggedInUser == null)
                {
                    logger.Warn($"CustomersPaymentsController - GET: cusinvPay/MyPayments - Not Found / invalid user, logged in UserName: {userName}");
                    return new NotFoundResponseFactory().CreateResponse("User not found");
                }

                int id = loggedInUser.EfUserId;
                CustomersInvoicePaymentsRepo prod = new CustomersInvoicePaymentsRepo(_context);
                var customersPayments = prod.GetInvoicePayments(id).ToList();

                return new SuccessResponseFactory().CreateResponse(customersPayments);
            }

            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }

        // GET: /cusinvPay
        [HttpGet]
        [EnableCors("AllowOrigin")]
        // [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            logger.Info("CustomersPaymentsController - GET all CustomersPayments: /cusinvPay");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var adminRoleCheck = new AdminRoleCheckStrategy(_identityHelper);
            bool isAdmin = await adminRoleCheck.CheckRole(userId);

            if (isAdmin)
            {
                CustomersInvoicePaymentsRepo prod = new CustomersInvoicePaymentsRepo(_context);
                var customersPayments = prod.GetInvoicePayments(0).ToList();

                return new SuccessResponseFactory().CreateResponse(customersPayments);
            }

            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = new List<string>(await _userManager.GetRolesAsync(user));

            logger.Error($"CustomersPaymentsController - GET all CustomersPayments: /cusinvPay - Not Authorized - logged in User: {user}");

            return new ErrorResponseFactory().CreateResponse(new
            {
                message = "Not Authorized",
                user.UserName,
                isAdmin,
                userRoles
            });
        }

        [EnableCors("AllowOrigin")]
        // GET: /cusinvPay/userId/5
        [HttpGet("userId/{id}")]
        // [Authorize(Roles = "Administrator", "Employee")]
        public async Task<IActionResult> Get(int id)
        {
            logger.Info($"CustomersPaymentsController - GET: cusinvPay/userId/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            var adminRoleCheck = new AdminRoleCheckStrategy(_identityHelper);
            var employeeRoleCheck = new EmployeeRoleCheckStrategy(_identityHelper);
            bool isAdmin = await adminRoleCheck.CheckRole(userId);
            bool isEmployee = await employeeRoleCheck.CheckRole(userId);

            if (!string.IsNullOrEmpty(userName) && (isAdmin || isEmployee))
            {
                CustomersInvoicePaymentsRepo prod = new CustomersInvoicePaymentsRepo(_context);
                var customersPayments = prod.GetInvoicePayments(id).ToList();

                if (customersPayments.Count > 0)
                {
                    return new SuccessResponseFactory().CreateResponse(customersPayments);
                }

                return new NotFoundResponseFactory().CreateResponse("Payment not found");
            }

            logger.Warn($"CustomersPaymentsController - GET: cusinvPay/userId/{id} - logged in User: {user}");

            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }

        [EnableCors("AllowOrigin")]
        // GET: /cusinvPay/PaymentInfo/5
        [HttpGet("PaymentInfo/{id}")]
        // [Authorize(Roles = "Administrator", "Employee", "Customer")]
        public async Task<IActionResult> GetPaymentInfo(int id)
        {
            logger.Info($"CustomersPaymentsController - GET: cusinvPay/PaymentInfo/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            if (!string.IsNullOrEmpty(userName))
            {
                CustomersInvoicePaymentsRepo prod = new CustomersInvoicePaymentsRepo(_context);
                var paymentInfo = prod.GetPaymentDetails(id);

                return new SuccessResponseFactory().CreateResponse(paymentInfo);
            }

            logger.Warn($"CustomersPaymentsController - GET: cusinvPay/PaymentInfo/{id} - logged in User: {user}");

            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }

        [EnableCors("AllowOrigin")]
        // GET: /cusinvPay/InvoiceId/5
        [HttpGet("InvoiceId/{id}")]
        // [Authorize(Roles = "Administrator", "Employee", "Customer")]
        public async Task<IActionResult> PaymentsByInvoiceId(int id)
        {
            logger.Info($"CustomersPaymentsController - GET: cusinvPay/InvoiceId/{id}");

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            string userName = user.UserName;

            if (!string.IsNullOrEmpty(userName))
            {
                CustomersInvoicePaymentsRepo prod = new CustomersInvoicePaymentsRepo(_context);
                var customersPayments = prod.GetPaymentsByInvoiceId(id).ToList();

                if (customersPayments.Count > 0)
                {
                    return new SuccessResponseFactory().CreateResponse(customersPayments);
                }

                return new NotFoundResponseFactory().CreateResponse($"Payments with user product id {id} not found");
            }

            logger.Warn($"CustomersPaymentsController - GET: cusinvPay/InvoiceId/{id} - logged in User: {user}");

            return new ErrorResponseFactory().CreateResponse("Not Authorized");
        }
    }
}