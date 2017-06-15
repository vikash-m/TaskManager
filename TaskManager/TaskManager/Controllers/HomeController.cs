using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;
using System.Web.Mail;
using PagedList;
using PagedList.Mvc;
using TaskDAL;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        EmployeeService employeeService = new EmployeeService();
        UserService userService = new UserService();
        
        [HttpGet]
        public ActionResult SaveUserDetails()
        {
            var roleResult = userService.DropdownRoles();
            IEnumerable<SelectListItem> roles=new SelectList(roleResult, "RoleId", "RoleName");
           // var RolRes = new SelectList(roleResult, "RoleId", "RoleName");
            ViewBag.List = roles;
            var mgrResult = userService.DropdownMgr();
            var MgrRes = new SelectList(mgrResult, "Id", "FirstName");
            ViewBag.List1 = MgrRes;
            return View();
        }
        [HttpPost]
        public ActionResult SaveUserDetails(UserdetailDm ud)
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (ModelState.IsValid)
            {
                var result = userService.SaveUsers(ud);
                if(result)
                {
                    return RedirectToAction("ViewUserDetails");
                }
                else
                {
                    return RedirectToAction("SaveUserDetails");
                }
                
            }
            else
            {
                return RedirectToAction("SaveUserDetails");
            }
        }
        public ActionResult ViewUserDetails(int? page)
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (null != user)
            {
                var result = userService.ViewUser().ToList().ToPagedList(page ?? 1, 10);
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        

        [HttpGet]
        public ActionResult EditUserDetails(int id)
        {
           var res= userService.EditUser(id);
            var roleResult = userService.DropdownRoles();
            IEnumerable<SelectListItem> roles = new SelectList(roleResult, "RoleId", "RoleName");
            // var RolRes = new SelectList(roleResult, "RoleId", "RoleName");
            ViewBag.List = roles;
            var mgrResult = userService.DropdownMgr();
            var MgrRes = new SelectList(mgrResult, "Id", "FirstName");
            ViewBag.List1 = MgrRes;
            return View(res);
        }
        [HttpPost]
        public ActionResult EditUserDetails(UserdetailDm udm)
        {
            var result = userService.SaveEditUser(udm);
            return RedirectToAction("ViewUserDetails");
        }
        public ActionResult DeleteUser(int id)
        {
            var result = userService.DeleteUser(id);
            return RedirectToAction("ViewUserDetails");
        }
    }
}