using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskDomain.DomainModel;

namespace TaskManager.Controllers
{

    public class LoginController : Controller
    {
        private static readonly string ServiceLayerUrl = $"{ConfigurationManager.AppSettings["serviceLayerUrl"]}";
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
                var loginUser = new LoginUserDm();
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

                // List data response.
                var response = await client.GetAsync($"/login/login-details/?name={name}&password={password}");
                if (response.IsSuccessStatusCode)
                {
                    loginUser = response.Content.ReadAsAsync<LoginUserDm>().Result;
                }

                var id = loginUser.EmpId;

                var userDetails = await UserDetailsData(id);
                if (userDetails != null)
                {
                    switch (userDetails.RoleId)
                    {
                        case (int)EnumClass.Roles.Employee:
                            Session["SessionData"] = userDetails;
                            return RedirectToAction("Dashboard", "Employee");

                        case (int)EnumClass.Roles.Manager:
                            Session["SessionData"] = userDetails;
                            return RedirectToAction("Dashboard", "Manager");
                        case (int)EnumClass.Roles.Admin:

                            Session["SessionData"] = userDetails;
                            return RedirectToAction("ViewUserDetails", "Admin");
                        default:
                            return RedirectToAction("");
                    }
                }
                ViewBag.message = "Invalid UserName/Password";
                return View();
            }
            catch
            {
                ViewBag.message = "Invalid UserName/Password";
                return View();
            }
        }

        public async Task<UserDetailDm> UserDetailsData(string id)
        {

            var userDetail = new UserDetailDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                var response = await client.GetAsync($"/login/{id}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    userDetail = response.Content.ReadAsAsync<UserDetailDm>().Result;

            }
            catch (Exception)
            {
                throw;
            }

            return userDetail;


        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}