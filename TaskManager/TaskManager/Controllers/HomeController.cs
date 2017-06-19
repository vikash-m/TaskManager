using System.Linq;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;

using PagedList;


namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService = new UserService();

        [HttpGet]
        public ActionResult SaveUserDetails()
        {
            var roleResult = _userService.DropdownRoles();
            var roles = new SelectList(roleResult, "RoleId", "RoleName");
            // var RolRes = new SelectList(roleResult, "RoleId", "RoleName");
            ViewBag.List = roles;
            var mgrResult = _userService.DropdownMgr();
            var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
            ViewBag.List1 = mgrRes;
            return View();
        }
        [HttpPost]
        public ActionResult SaveUserDetails(UserdetailDm ud)
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (!ModelState.IsValid) return RedirectToAction("SaveUserDetails");
            var result = _userService.SaveUsers(ud);
            return RedirectToAction(result ? "ViewUserDetails" : "SaveUserDetails");
        }
        public ActionResult ViewUserDetails(int? page)
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (null == user) return RedirectToAction("Login", "Login");
            var result = _userService.ViewUser().ToList().ToPagedList(page ?? 1, 10);
            return View(result);
        }


        [HttpGet]
        public ActionResult EditUserDetails(int id)
        {
            var res = _userService.EditUser(id);
            var roleResult = _userService.DropdownRoles();
            var roles = new SelectList(roleResult, "RoleId", "RoleName");
            // var RolRes = new SelectList(roleResult, "RoleId", "RoleName");
            ViewBag.List = roles;
            var mgrResult = _userService.DropdownMgr();
            var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
            ViewBag.List1 = mgrRes;
            return View(res);
        }
        [HttpPost]
        public ActionResult EditUserDetails(UserdetailDm udm)
        {
            _userService.SaveEditUser(udm);
            return RedirectToAction("ViewUserDetails");
        }
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("ViewUserDetails");
        }
    }
}