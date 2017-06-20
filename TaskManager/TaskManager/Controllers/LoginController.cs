using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;

namespace TaskManager.Controllers
{
    // enum Roles { Admin = 1, Manager, Employee };
    public class LoginController : Controller
    {
        static HttpClient client = new HttpClient();
        private static string serviceLayerUrl = ConfigurationManager.AppSettings["serviceLayerUrl"] + "/LoginService";
        private string urlParameters;

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginUserDm log)
        {
            //To Do: discuss this condition
            if (!ModelState.IsValid) return View();

            try
            {
                var name = log.UserName;
                var password = log.Password;

                string URL = serviceLayerUrl + "/GetLogDetails";
                HttpClient client = new HttpClient();
                urlParameters = "?name=" + name + "&password=" + password;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                var result = response.Content.ReadAsAsync<LoginUserDm>().Result;
                var id = (int)result.EmpId;

                var userDetails = UserDetailsData(id);
                if (userDetails != null)
                {
                    switch (userDetails.RoleId)
                    {
                        case (long)EnumClass.Roles.Employee:

                            Session["SessionData"] = userDetails;
                            return RedirectToAction("Dashboard", "Employee");
                        case (long)EnumClass.Roles.Manager:
                            Session["SessionData"] = userDetails;
                            return RedirectToAction("Dashboard", "Manager");
                        case (long)EnumClass.Roles.Admin:

                            Session["SessionData"] = userDetails;
                            return RedirectToAction("ViewUserDetails", "Home");
                        default:
                            return RedirectToAction("");
                    }
                }
                ViewBag.message = "Invalid UserName/Password";
                return View();
            }
            catch (Exception)
            {
                ViewBag.message = "Invalid UserName/Password";
                return View();
            }
        }

        public UserdetailDm UserDetailsData(int id)
        {
            var logServices = new LoginServices();
            var userDataResult = logServices.GetUserDetailsData(id);
            return userDataResult;

        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}