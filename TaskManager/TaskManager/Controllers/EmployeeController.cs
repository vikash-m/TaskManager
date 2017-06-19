using System.IO;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;
using PagedList;


namespace TaskManager.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private readonly EmployeeService _employeeService = new EmployeeService();

        public ActionResult Dashboard()
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (null == user) return RedirectToAction("Login", "Login");
            var taskStatusCounts = _employeeService.GetTaskCounts(user.Id);
            return View(taskStatusCounts);
        }

        public ActionResult MyTasks(int? page)
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (null == user) return RedirectToAction("Login", "Login");
            var employeeTasks = _employeeService.GetEmployeeTasks(user.Id).ToPagedList(page ?? 1, 5);
            return View(employeeTasks);
        }
        [HttpPost]
        public JsonResult GetStatusList()
        {
            var statusList = _employeeService.GetStatusList();
            return Json(new { data = statusList });
        }
        [HttpPost]
        public bool UpdateTask(long id, long status)
        {
            var result = _employeeService.UpdateTask(id, status);
            return result;
        }

        [HttpGet]
        public ActionResult GetTaskDetails(long id)
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (null == user) return RedirectToAction("Login", "Login");
            var singleTaskDetails = _employeeService.GetTaskDetails(id);
            return View(singleTaskDetails);
            // var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/uploads/"));
            // System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            // List<string> items = new List<string>();
            // foreach (var file in fileNames)
            // {
            //     items.Add(file.Name);
            // }
            //// ViewBag.Downloads = items;
            // List<string> extension = new List<string>();
            // extension.Add("png");
            // extension.Add("pdf");
            // extension.Add("text");
            // ViewBag.Downloads = extension;

        }


        public FileResult Download(string fileName)


        {
            // var FileVirtualPath = "~/App_Data/Uploads/" + FileName;
            return File(fileName, "application/force-download", Path.GetFileName(fileName));
        }
    }
}