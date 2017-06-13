using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;
using PagedList;
using PagedList.Mvc;

namespace TaskManager.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeService employeeService = new EmployeeService();
        
        public ActionResult Dashboard()
        {
            var user = (UserdetailDm)Session["SessionData"];
            var taskStatusCounts = employeeService.GetTaskCounts(user.Id);
            return View(taskStatusCounts);
        }

        public ActionResult MyTasks(int ?page)
        {
            var user = (UserdetailDm)Session["SessionData"];
            var employeeTasks = employeeService.GetEmployeeTasks(user.Id).ToPagedList(page ?? 1,5);
            return View(employeeTasks);
        }
        [HttpPost]
        public JsonResult GetStatusList()
        {
            var statusList = employeeService.GetStatusList();
            return Json(new { data = statusList });
        }
        [HttpPost]
        public bool UpdateTask(long id, long status)
        {
            var result = employeeService.UpdateTask(id, status);
            return result;
        }

        [HttpGet]
        public ActionResult GetTaskDetails(long Id)
        {
            var SingleTaskDetails = employeeService.GetTaskDetails(Id);
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/uploads/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
           // ViewBag.Downloads = items;
            List<string> extension = new List<string>();
            extension.Add("png");
            extension.Add("pdf");
            extension.Add("text");
            ViewBag.Downloads = extension;
            return View(SingleTaskDetails);
        }

       
        public FileResult Download(string ImageName)


        {
            var FileVirtualPath = "~/App_Data/Uploads/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }
    }
}