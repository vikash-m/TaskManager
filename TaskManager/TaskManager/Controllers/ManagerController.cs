using System.Web;
using System.Web.Mvc;
using TaskServiceLayer;
using TaskDomain.DomainModel;
using System.IO;

namespace TaskManager.Controllers
{
    public class ManagerController : Controller
    {
        ManagerService ManagerService = new ManagerService();
        // GET: Manager
        public ActionResult ListTask()
        {
            var taskList = ManagerService.GetAllTask();          
            return View(taskList);
        }

        public ActionResult Dashboard()
        {
            return View();

        }
        public ActionResult CreateTask()
        {
            long ManagerId = 4;
            var employeeList = ManagerService.GetEmployeesDetailsByManagerId(ManagerId);          
            ViewBag.Employee = new SelectList(employeeList,"Id", "FirstName", "LastName");
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(TaskDm taskDm)
        {
            var loginUser  = (UserdetailDm)Session["SessionData"];
            taskDm.TaskStatusId = (long)Enum.Enum.Status.Pending;
            var result = ManagerService.AddTask(taskDm, loginUser.Id);

            return RedirectToAction("ListTask");
        }
        [HttpPut]
        public ActionResult UpdateTask(TaskDm taskDm)
        {                  
            var result = ManagerService.UpdateTask(taskDm);

            return RedirectToAction("ListTask");
        }
        [HttpPut]
        public ActionResult DeleteTask(TaskDm taskDm)
        {
            var result = ManagerService.UpdateTask(taskDm);

            return RedirectToAction("ListTask");
        }
       
        public ActionResult DocumentPartialView(int? id)
        {
            var taskName = ManagerService.GetTaskNameByTaskId(id);
            var taskDocument = new TaskDocumentDm
            {
                TaskId = (long)id,
                TaskTitle = taskName                
            };
            return PartialView("_AddDocument",taskDocument);
        }
        [HttpPost]
        public ActionResult AddDocumentDetails(TaskDocumentDm taskDocument)
        {

            var loginUser = (UserdetailDm)Session["SessionData"];
            foreach (var file in taskDocument.Document)
            {
                if (file != null)
                {
                    var filePath = Path.Combine(Server.MapPath("~/TaskDocumentFile/ "), taskDocument.TaskTitle);
                   // var filePath = Server.MapPath("~/E:/Task");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    file.SaveAs(filePath);
                    taskDocument.DocumentPath = filePath;
                    ManagerService.AddTaskDocument(taskDocument, loginUser.Id);
                }
            }
           

                  
           // var result = ManagerService.AddTaskDocument(taskDocument, loginUser.Id);

            return RedirectToAction("ListTask");
        }
    }
}