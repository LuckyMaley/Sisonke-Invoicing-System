using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SISONKE_Invoicing_ASPNET.Areas.Administrator.Models;
using SISONKE_Invoicing_ASPNET.Areas.Employee.Models;
using SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Models;
using SISONKE_Invoicing_ASPNET.Models;
using SISONKE_Invoicing_ASPNET.Services;

namespace SISONKE_Invoicing_ASPNET.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CrudAdminsController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("CrudAdminsController");

        private readonly ClientSettings _clientSettings;

        private readonly HttpClient _httpClient;
        public CrudAdminsController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)

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

                AdminInvoiceProductVM invoiceProductsVM = new AdminInvoiceProductVM();

                invoiceProductsVM.Products = new List<Product>();

                invoiceProductsVM.Invoices = new List<Invoice>();

                invoiceProductsVM.InvoiceItems = new List<InvoiceItem>();

                invoiceProductsVM.Users = new List<UserProfileVM>();



                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Administrator")

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


                    string apiUrlProfile = baseUrl + "/api/UserProfile/AllUsersCheck";

                    

                    List<UserProfileVM> currentUserProfile = new List<UserProfileVM>();

                    resp = await _client.GetAsync(apiUrlProfile);

                    if (resp.IsSuccessStatusCode)

                    {

                        var result = resp.Content.ReadAsStringAsync().Result;

                        currentUserProfile = JsonConvert.DeserializeObject<List<UserProfileVM>>(result);
                        if(currentUserProfile != null)
                        {
                            invoiceProductsVM.Users = currentUserProfile;
                        }

                    }


                    apiUrlUsersProductsInvoices = baseUrl + $"/api/Invoices";

                    resp = await _client.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {



                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<Invoice>>(results);

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


        public async Task<IActionResult> Invoices()
        {

            try

            {


                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();

                AdminInvoiceProductVM invoiceProductsVM = new AdminInvoiceProductVM();

                invoiceProductsVM.Products = new List<Product>();

                invoiceProductsVM.Invoices = new List<Invoice>();

                invoiceProductsVM.InvoiceItems = new List<InvoiceItem>();


                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Administrator")

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




                    apiUrlUsersProductsInvoices = baseUrl + $"/api/Invoices";

                    resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {



                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<Invoice>>(results);

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
        public async Task<IActionResult> Details(int id)
        {



            return View();
        }
        public async Task<IActionResult> Products()
        {
            try

            {


                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();

                AdminInvoiceProductVM invoiceProductsVM = new AdminInvoiceProductVM();

                invoiceProductsVM.Products = new List<Product>();

                invoiceProductsVM.Invoices = new List<Invoice>();

                invoiceProductsVM.InvoiceItems = new List<InvoiceItem>();


                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Administrator")

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




                    apiUrlUsersProductsInvoices = baseUrl + $"/api/Invoices";

                    resp = await _httpClient.GetAsync(apiUrlUsersProductsInvoices);

                    if (resp.IsSuccessStatusCode)

                    {

                        var results = await resp.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(results))

                        {

                            try

                            {



                                var customersInvoiceDetailsVMList = JsonConvert.DeserializeObject<List<Invoice>>(results);

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


        public async Task<IActionResult> Customer()
        {

            try

            {


                string _userLoginToken = TempData["UserToken"]?.ToString();

                string _userLoginRole = TempData["UserRole"]?.ToString();

                List<UserProfileVM> currentUserProfile = new List<UserProfileVM>();

                if (string.IsNullOrEmpty(_userLoginToken) || _userLoginRole != "Administrator")

                {

                    TempData["UserHomePageUrl"] = "/Home/Index";

                    TempData.Keep();

                    return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });

                }

                else

                {

                    string baseUrl = _clientSettings.ClientBaseUrl;

                    

                    HttpClient _client = new HttpClient().AddBearerToken(_userLoginToken);

                     

                    string apiUrlProfile = baseUrl + "/api/UserProfile/AllUsersCheck";



                    

                    HttpResponseMessage resp = await _client.GetAsync(apiUrlProfile);

                    if (resp.IsSuccessStatusCode)

                    {

                        var result = resp.Content.ReadAsStringAsync().Result;

                        currentUserProfile = JsonConvert.DeserializeObject<List<UserProfileVM>>(result);
                      

                    }

                }



                return View(currentUserProfile);

            }

            catch (Exception e)

            {

                logger.Error("Error in InvoiceHistory method", e);

                return View();

            }

        }


    }
    //completed
}
