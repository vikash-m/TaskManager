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
            if (ModelState.IsValid)
            {


                LoginServices logServices = new LoginServices();
                string name = log.UserName;
                string password = log.Password;
                try
                {
                    var result = logServices.getLogDetails(name, password);

                    int Id = (int)result.EmpId;

                    var UserDetails = UserDetailsData(Id);
                    if (UserDetails != null)
                    {
                        if (UserDetails.RoleId == (long)EnumClass.Roles.Employee)
                        {

                            Session["SessionData"] = UserDetails;
                            return RedirectToAction("Dashboard", "Employee");
                        }
                        else if (UserDetails.RoleId == (long)EnumClass.Roles.Manager)
                        {
                            Session["SessionData"] = UserDetails;
                            return RedirectToAction("Dashboard", "Manager");
                        }
                        else if (UserDetails.RoleId == (long)EnumClass.Roles.Admin)
                        {

                            Session["SessionData"] = UserDetails;
                            return RedirectToAction("ViewUserDetails", "Home");
                        }
                        else
                        {
                            return RedirectToAction("");
                        }
                    }
                    ViewBag.message = "Invalid UserName/Password";
                    return View();
                }
                catch (Exception e)
                {
                    ViewBag.message = "Invalid UserName/Password";
                    return View();
                }

            }

            else
            {

                return View();
            }

        }

        public UserdetailDm UserDetailsData(int Id)
        {
            LoginServices logServices = new LoginServices();
            var UserDataResult = logServices.GetUserDetailsData(Id);
            return UserDataResult;

        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}