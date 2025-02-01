using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SISONKE_Invoicing_ASPNET.Areas.Customer.Models;
using SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Models;
using SISONKE_Invoicing_ASPNET.Models;
using SISONKE_Invoicing_ASPNET.Services;

namespace SISONKE_Invoicing_ASPNET.Areas.Customer.Controllers

{

    [Area("Customer")]

    public class CrudCustomersController : Controller

    {
        private static readonly ILog logger = LogManager.GetLogger("CrudCustomersController");

        private readonly ClientSettings _clientSettings;

        private readonly HttpClient _httpClient;



        public CrudCustomersController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)

        {

            _clientSettings = ctSettings.Value;

            _httpClient = injectedClient;

        }

        public async Task<IActionResult> Index()
        {

            try
            {


                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();

                TempData.Keep();

                InvoiceProductsVM invoiceProductsVM = new InvoiceProductsVM();

                invoiceProductsVM.Products = new List<Product>();

                invoiceProductsVM.Invoices = new List<MyInvoices>();

                invoiceProductsVM.InvoiceItems = new List<InvoiceItem>();


                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Customer")

                {

                    TempData["UserHomePageUrl"] = "/Home/Index";

                    TempData.Keep();

                    return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });

                }

                else

                {

                    string baseUrl = _clientSettings.ClientBaseUrl;

                    string apiUrlUsersProductsInvoices = baseUrl + "/api/Products";

                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    HttpResponseMessage resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {


                                var products = JsonConvert.DeserializeObject<List<Product>>(results);

                                if (products != null)

                                {

                                    invoiceProductsVM.Products = products;

                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing UsersProductsInvoicesVM", ex);

                            }

                        }

                    }




                    apiUrlUsersProductsInvoices = baseUrl + $"/api/Invoices/MyInvoice";

                    resp = await _client.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {



                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<MyInvoices>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoiceProductsVM.Invoices = customersInvoiceDetailsVMList;


                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                    apiUrlUsersProductsInvoices = baseUrl + $"/api/InvoiceItems";

                    resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {

                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<InvoiceItem>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoiceProductsVM.InvoiceItems = customersInvoiceDetailsVMList;


                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                }



                return View(invoiceProductsVM);

            }

            catch (Exception e)

            {

                logger.Error("Error in InvoiceHistory method", e);

                return View();

            }

        }


        public async Task<IActionResult> CustomerInvoices()
        {

            try
            {


                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();
                TempData.Keep();

                InvoiceProductsVM invoiceProductsVM = new InvoiceProductsVM();

                invoiceProductsVM.Products = new List<Product>();

                invoiceProductsVM.Invoices = new List<MyInvoices>();

                invoiceProductsVM.InvoiceItems = new List<InvoiceItem>();


                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Customer")

                {

                    TempData["UserHomePageUrl"] = "/Home/Index";

                    TempData.Keep();

                    return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });

                }

                else

                {

                    string baseUrl = _clientSettings.ClientBaseUrl;

                    string apiUrlUsersProductsInvoices = baseUrl + "/api/Products";

                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    HttpResponseMessage resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {


                                var products = JsonConvert.DeserializeObject<List<Product>>(results);

                                if (products != null)

                                {

                                    invoiceProductsVM.Products = products;

                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing UsersProductsInvoicesVM", ex);

                            }

                        }

                    }




                    apiUrlUsersProductsInvoices = baseUrl + $"/api/Invoices/MyInvoice";

                    resp = await _client.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {



                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<MyInvoices>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoiceProductsVM.Invoices = customersInvoiceDetailsVMList;


                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                    apiUrlUsersProductsInvoices = baseUrl + $"/api/InvoiceItems";

                    resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {

                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<InvoiceItem>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoiceProductsVM.InvoiceItems = customersInvoiceDetailsVMList;


                                }


                            }
                            catch (JsonException ex)
                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                }



                return View(invoiceProductsVM);

            }

            catch (Exception e)

            {

                logger.Error("Error in InvoiceHistory method", e);

                return View();

            }

        }







        public async Task<IActionResult> Profile()

        {

            try

            {

                string baseUrl = _clientSettings.ClientBaseUrl;

                string apiUrlProfile = baseUrl + "/api/UserProfile";

                string userHomePageUrl;

                UserProfileVM currentUserProfile = new UserProfileVM();

                string _userLoginToken = TempData["UserToken"]?.ToString();

                if (string.IsNullOrEmpty(_userLoginToken))

                {

                    TempData["UserHomePageUrl"] = "/Home/Index";

                    TempData.Keep();

                    return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });

                }

                else

                {

                    TempData.Keep();

                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    HttpResponseMessage resp = await _client.GetAsync(apiUrlProfile);

                    if (resp.IsSuccessStatusCode)

                    {

                        var result = resp.Content.ReadAsStringAsync().Result;

                        currentUserProfile = JsonConvert.DeserializeObject<UserProfileVM>(result);

                    }

                    return View(currentUserProfile);

                }

            }

            catch (Exception e)

            {

                return View();

            }

        }




        [HttpPost]
        public async Task<IActionResult> Profile(UpdateUserProfileVM updateProfileVM)
        {
            try
            {
                logger.Info("AccountController: Profile Action");
                string _userLoginToken = TempData["UserToken"]?.ToString();
                string _userLoginRole = TempData["UserRole"]?.ToString();

                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Customer")
                {
                    TempData["UserHomePageUrl"] = "/Home/Index";
                    TempData.Keep();
                    return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });
                }
                else
                {
                    string baseUrl = _clientSettings.ClientBaseUrl;
                    string apiUrlProfile = baseUrl + "/api/UserProfile";

                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    var putResponse = _client.PutAsJsonAsync(apiUrlProfile, updateProfileVM);
                    putResponse.Wait();
                    var result = putResponse.Result;

                    if (result.IsSuccessStatusCode)
                    {


                        TempData["FirstName"] = updateProfileVM.FirstName;
                        TempData["LastName"] = updateProfileVM.LastName;


                        TempData.Keep();

                        return RedirectToAction(nameof(Index));
                    }

                    return View(updateProfileVM);
                }
            }
            catch (Exception e)
            {
                logger.Error("Error in Profile method", e);
                return View();
            }
        }






        public async Task<IActionResult> CustomerPayments()
        {

            try
            {


                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();
                TempData.Keep();


                InvoicePaymentsVM invoicePaymentsVM = new InvoicePaymentsVM();

                invoicePaymentsVM.Products = new List<Product>();

                invoicePaymentsVM.Invoices = new List<MyInvoices>();

                invoicePaymentsVM.InvoiceItems = new List<InvoiceItem>();

                invoicePaymentsVM.Payments = new List<MyPayments>();


                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Customer")

                {

                    TempData["UserHomePageUrl"] = "/Home/Index";

                    TempData.Keep();

                    return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });

                }

                else

                {

                    string baseUrl = _clientSettings.ClientBaseUrl;

                    string apiUrlUsersProductsInvoices = baseUrl + "/api/Products";

                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    HttpResponseMessage resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {


                                var products = JsonConvert.DeserializeObject<List<Product>>(results);

                                if (products != null)

                                {

                                    invoicePaymentsVM.Products = products;

                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing UsersProductsInvoicesVM", ex);

                            }

                        }

                    }




                    apiUrlUsersProductsInvoices = baseUrl + $"/cusinvPay/MyPayments";

                    resp = await _client.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {



                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<MyPayments>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoicePaymentsVM.Payments = customersInvoiceDetailsVMList;


                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                    apiUrlUsersProductsInvoices = baseUrl + $"/api/Invoices/MyInvoice";

                    resp = await _client.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {



                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<MyInvoices>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoicePaymentsVM.Invoices = customersInvoiceDetailsVMList.Where(c => c.Status != "Paid").ToList();


                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }


                    apiUrlUsersProductsInvoices = baseUrl + $"/api/InvoiceItems";

                    resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {

                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<InvoiceItem>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoicePaymentsVM.InvoiceItems = customersInvoiceDetailsVMList;


                                }


                            }
                            catch (JsonException ex)
                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                }



                return View(invoicePaymentsVM);

            }

            catch (Exception e)

            {

                logger.Error("Error in InvoiceHistory method", e);

                return View();

            }

        }

        public async Task<IActionResult> MakePayment(int id)

        {

            try

            {


                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();

                TempData.Keep();

                InvoiceProductsVM invoiceProductsVM = new InvoiceProductsVM();

                invoiceProductsVM.Products = new List<Product>();

                invoiceProductsVM.Invoices = new List<MyInvoices>();

                invoiceProductsVM.InvoiceItems = new List<InvoiceItem>();

                PaymentVM paymentVM = new PaymentVM();

                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Customer")

                {

                    TempData["UserHomePageUrl"] = "/Home/Index";

                    TempData.Keep();

                    return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });

                }

                else

                {

                    string baseUrl = _clientSettings.ClientBaseUrl;

                    string apiUrlUsersProductsInvoices = baseUrl + "/api/Products";

                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    HttpResponseMessage resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {


                                var products = JsonConvert.DeserializeObject<List<Product>>(results);

                                if (products != null)

                                {

                                    invoiceProductsVM.Products = products;

                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing UsersProductsInvoicesVM", ex);

                            }

                        }

                    }



                    apiUrlUsersProductsInvoices = baseUrl + $"/api/Invoices/MyInvoice";

                    resp = await _client.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {


                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<MyInvoices>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoiceProductsVM.Invoices = customersInvoiceDetailsVMList;



                                    var invoice = customersInvoiceDetailsVMList.FirstOrDefault(x => x.InvoiceId == id);

                                    paymentVM.InvoiceId = id;

                                    paymentVM.PaymentDate = DateTime.Now;

                                    paymentVM.PaymentMethod = "debit-card";

                                    paymentVM.Amount = invoice.TotalAmount;

                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                    apiUrlUsersProductsInvoices = baseUrl + $"/api/InvoiceItems";

                    resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {

                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<InvoiceItem>>(results);

                                if (customersInvoiceDetailsVMList != null)

                                {

                                    invoiceProductsVM.InvoiceItems = customersInvoiceDetailsVMList;


                                }


                            }

                            catch (JsonException ex)

                            {

                                logger.Error("Error deserializing CustomersInvoiceDetailsVM", ex);

                            }

                        }

                    }

                }


                return View(paymentVM);

            }

            catch (Exception e)

            {

                logger.Error("Error in InvoiceHistory method", e);

                return View();

            }



        }


        [HttpPost]

        public async Task<IActionResult> MakePayment(PaymentVM payment)

        {

            try

            {

                List<Payment> allPayments = new List<Payment>();

                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();

                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Customer")

                {

                    TempData["UserHomePageUrl"] = "/Home/Index";

                    TempData.Keep();

                    return RedirectToAction("Login",

                   "UserManager"

                   , new { area = "SecurityServices" });

                }

                else

                {

                    custInvoiceVM invoice = new custInvoiceVM();

                    string baseUrl = _clientSettings.ClientBaseUrl;

                    string apiUrlPayment = baseUrl + "/api/Payments";

                    invoice.Status = "Paid";

                    string apiUrlInvoice = baseUrl + $"/api/Invoices/invPay/{payment.InvoiceId}";


                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                    var putResponse = _client.PutAsJsonAsync(apiUrlInvoice, invoice);


                    putResponse.Wait();

                    var resultInvoice = putResponse.Result;

                    
                    var postResponse = _client.PostAsJsonAsync(apiUrlPayment, payment);

                    postResponse.Wait();

                    var result = postResponse.Result;

                    if (result.IsSuccessStatusCode)

                    {

                        return RedirectToAction("CustomerPayments", "CrudCustomers", new { area = "Customer" });

                    }

                    return View();

                }

            }

            catch (Exception e)

            {

                return View();

            }

        }


    }


}
