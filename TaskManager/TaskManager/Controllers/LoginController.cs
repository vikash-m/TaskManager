using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;

namespace TaskManager.Controllers { 
enum Roles { Admin=1,Manager,Employee };
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
                var result = logServices.getLogDetails(name, password);
               
                if (result != null)
                {
                    int Id = (int)log.Id;
                    if (result.RoleId == (long)Roles.Employee)
                    {
                        var UserDetails = UserDetailsData(Id);
                            
                        Session["SessionData"] = UserDetails;
                        return RedirectToAction("Dashboard","Manager");
                    }
                    else if (result.RoleId ==(long)Roles.Manager)
                    {
                        var UserDetails = UserDetailsData(Id);

                        Session["SessionData"] = UserDetails;
                        return RedirectToAction("");
                    }
                    else if (result.RoleId == (long)Roles.Admin)
                    {
                        var UserDetails = UserDetailsData(Id);

                        Session["SessionData"] = UserDetails;
                        return RedirectToAction("");
                    }
                    else
                    {
                        return RedirectToAction("");
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        public UserdetailDm UserDetailsData(int Id )
        {
            LoginServices logServices = new LoginServices();
             var UserDataResult =logServices.GetUserDetailsData(Id);
            return UserDataResult; 

        }
    }
}