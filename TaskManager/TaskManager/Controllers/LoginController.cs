using System;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;

namespace TaskManager.Controllers
{
    // enum Roles { Admin = 1, Manager, Employee };
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginUserDm log)
        {
            //To Do: discuss this condition
            if (!ModelState.IsValid) return View();
            var logServices = new LoginServices();
            var name = log.UserName;
            var password = log.Password;
            try
            {
                var result = logServices.GetLogDetails(name, password);

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