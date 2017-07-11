using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;
using TaskDomain.DomainModel;
using TaskManager.Content.Resources;

namespace TaskManager.Controllers
{

    public class LoginController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string ServiceLayerUrl = ServiceLayerLinkResource.serviceLayerUrl;
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
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.loginUrl , name , password));
                if (response.IsSuccessStatusCode)
                {
                    loginUser = response.Content.ReadAsAsync<LoginUserDm>().Result;
                }

                var id = loginUser.EmpId;
                logger.Info("Logger is working Fine");
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
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
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
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.userDetailsDataUrl , id));

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    userDetail = response.Content.ReadAsAsync<UserDetailDm>().Result;

            }
            catch (Exception ex)
            {
                logger.Error(ex,"Error Ocuured");
               
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