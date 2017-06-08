﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskManager.Enum;
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
                var result = logServices.getLogDetails(name, password);
                int Id = (int)log.EmpId;

                var UserDetails = UserDetailsData(Id); ;
                if (UserDetails != null)
                {
                    if (UserDetails.RoleId == (long)EnumRoles.Roles.Employee)
                    {

                        Session["SessionData"] = UserDetails;
                        return RedirectToAction("Dashboard", "Manager");
                    }
                    else if (UserDetails.RoleId == (long)EnumRoles.Roles.Manager)
                    {
                        Session["SessionData"] = UserDetails;
                        return RedirectToAction("");
                    }
                    else if (UserDetails.RoleId == (long)EnumRoles.Roles.Admin)
                    {

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