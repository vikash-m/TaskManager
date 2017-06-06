using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;

namespace TaskManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginUserDm log)
        {
            LoginServices logServices = new LoginServices();
            string name = log.UserName;
            string password = log.Password;
            var result = logServices.getLogDetails(name, password);
            if (result != null)
            {
                if (result.RoleName == "Employee")
                {
                    return RedirectToAction("");
                }
                else if (result.RoleName == "Manager")
                {
                    return RedirectToAction("");
                }
                else if (result.RoleName == "Admin")
                {
                    return RedirectToAction("");
                }
                else
                {
                    return RedirectToAction("");
                }
            }
            return View();
        }

    }
}