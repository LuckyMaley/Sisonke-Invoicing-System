using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Models;
using SISONKE_Invoicing_ASPNET.Services;

namespace SISONKE_Invoicing_ASPNET.Areas.SecurityServices.Controllers
{
    [Area("SecurityServices")]
    public class UserManagerController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger("UserManagerController");

        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _httpClient;

        public UserManagerController(IOptions<ClientSettings> ctSettings, HttpClient injectedClient)
        {
            _clientSettings = ctSettings.Value;
            _httpClient = injectedClient;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        // POST: Register page
        [HttpPost]
        public IActionResult Register(RegistrationVM usrRegData)
        {
            logger.Info("SecurityServices UserManagerController - Register");
            try
            {
                UserRegisteredVM newlyRegUser = new UserRegisteredVM();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrlRegister = baseUrl + "/api/ApplicationUser/Register";

                var postResponse = _httpClient.PostAsJsonAsync<RegistrationVM>(apiUrlRegister, usrRegData);
                postResponse.Wait();
                var result = postResponse.Result;

                ViewBag.POSTresultHeader = result.Headers;
                ViewBag.POSTresultStatusCode = result.StatusCode;
                ViewBag.POSTresultRequestMessage = result.RequestMessage;
                ViewBag.POSTresultIsSuccessStatusCode = result.IsSuccessStatusCode;

                if (result.IsSuccessStatusCode)
                {
                    logger.Info("SecurityServices UserManagerController - Register Successful: user added: "+ newlyRegUser.UserName);
                    var results = result.Content.ReadAsStringAsync().Result;
                    newlyRegUser = JsonConvert.DeserializeObject<UserRegisteredVM>(results);
                    LoginVM loginVM = new LoginVM()
                    {
                        UserName = usrRegData.UserName,
                        Password = usrRegData.Password
                    };
                    TempData["POSTRegUserUserName"] = newlyRegUser.UserName;
                    TempData.Keep();
                    return Login(loginVM);
                }
                else
                {
                    
                    return View();
                }
            }
            catch (Exception e)
            {
                logger.Error("SecurityServices UserManagerController - Register Not Successful");
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: Login page
        [HttpPost]
        public IActionResult Login(LoginVM usrLogin)
        {
            if(usrLogin.UserName == null)
            {
                return View();
            }
            logger.Info("AuthServices UserManagerController - Login - POST: Login page usrLogin.UserName: " + usrLogin.UserName);

            try
            {
                CurrentUserVM currentLoggedInUser = new CurrentUserVM();
                string baseUrl = _clientSettings.ClientBaseUrl;
                string apiUrlLogin = baseUrl + "/api/ApplicationUser/Login";

                string userHomePageUrl = baseUrl + "/Home/Index";

                var postResponse = _httpClient.PostAsJsonAsync<LoginVM>(apiUrlLogin, usrLogin);
                postResponse.Wait();
                var result = postResponse.Result;


                ViewBag.POSTresultHeader = result.Headers;
                ViewBag.POSTresultStatusCode = result.StatusCode;
                ViewBag.POSTresultRequestMessage = result.RequestMessage;
                ViewBag.POSTresultIsSuccessStatusCode = result.IsSuccessStatusCode;


                if (result.IsSuccessStatusCode)
                {
                    var results = result.Content.ReadAsStringAsync().Result;
                    currentLoggedInUser = JsonConvert.DeserializeObject<CurrentUserVM>(results);

                    TempData["UserHomePageUrl"] = userHomePageUrl;
                    TempData["UserToken"] = currentLoggedInUser.Token;
                    TempData["UserTokenValidTo"] = currentLoggedInUser.Expiration;
                    TempData["FirstName"] = currentLoggedInUser.FirstName;
                    TempData["LastName"] = currentLoggedInUser.LastName;
                    TempData["UserName"] = currentLoggedInUser.UserName;
                    TempData["UserRole"] = currentLoggedInUser.Roles[0];
                    TempData.Keep();

                }


                if (result.IsSuccessStatusCode)
                {
                    logger.Info("AuthServices UserManagerController - Login - POST: Login page usrLogin.UserName: " + usrLogin.UserName);
                    if (currentLoggedInUser != null && currentLoggedInUser.Roles[0] == "Administrator")
                    {
                        TempData["UserHomePageUrl"] = "/Administrator/CrudAdmins/Index";
                        TempData.Keep();
                        return RedirectToAction("Index", "CrudAdmins", new { area = "Administrator" });
                    }
                    else if (currentLoggedInUser != null && currentLoggedInUser.Roles[0] == "Employee")
                    {
                        TempData["UserHomePageUrl"] = "/Employee/CrudEmployees/Index";
                        TempData.Keep();
                        return RedirectToAction("Index", "CrudEmployees", new { area = "Employee" });
                    }
                    else
                    {
                        TempData["UserHomePageUrl"] = "/Customer/CrudCustomers/Index";
                        return RedirectToAction("Index", "CrudCustomers", new { area = "Customer" });
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return View();
            }

        }

        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Index", "Home", new { area = "default" });
        }

        public IActionResult LogoutTwo()
        {
            TempData.Clear();
            return RedirectToAction("Login", "UserManager", new { area = "SecurityServices" });
        }

        public async Task<IActionResult> UserProfile()
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
    }
}
