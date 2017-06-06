using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        EmployeeService employeeService = new EmployeeService();
       // UserService userService = new UserService();
        public ActionResult Index()
        {
            //getData();--zafar
            return View();
        }

        public ActionResult Index1()
        {
            var result = employeeService.GetEmployees();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        //public ActionResult SaveUserDetails()
        //{
        //    var roleResult = userService.DropdownRoles();
        //    var RolRes = new SelectList(roleResult, "RoleId", "RoleName");
        //    ViewBag.List = RolRes;
        //    var mgrResult = userService.DropdownMgr();
        //    var MgrRes = new SelectList(mgrResult, "Id", "FirstName");
        //    ViewBag.List1 = MgrRes;
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult SaveUserDetails(UserdetailDm ud)
        //{
        //    var result = userService.SaveUsers(ud);

        //    return RedirectToAction("SaveUserDetails");
        //}
        public ActionResult ViewUserDetails()
        {
            return View();
        }
    }
}