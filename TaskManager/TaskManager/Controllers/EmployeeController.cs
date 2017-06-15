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
            if (null != user)
            {
                
                var taskStatusCounts = employeeService.GetTaskCounts(user.Id);
                return View(taskStatusCounts);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult MyTasks(int ?page)
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (null != user)
            {
                var employeeTasks = employeeService.GetEmployeeTasks(user.Id).ToPagedList(page ?? 1, 5);
                return View(employeeTasks);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
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
            var user = (UserdetailDm)Session["SessionData"];
            if (null != user)
            {
                var SingleTaskDetails = employeeService.GetTaskDetails(Id);
                return View(SingleTaskDetails);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
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

       
        public FileResult Download(string FileName)


        {
           // var FileVirtualPath = "~/App_Data/Uploads/" + FileName;
            return File(FileName, "application/force-download", Path.GetFileName(FileName));
        }
    }
}