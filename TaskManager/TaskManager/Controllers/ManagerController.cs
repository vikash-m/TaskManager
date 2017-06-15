using PagedList;
using System.IO;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;
namespace TaskManager.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ManagerService _managerService = new ManagerService();
        // GET: Manager
        public ActionResult ListTask(int? page)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            var taskList = _managerService.GetAllTask(user.Id).ToPagedList(page ?? 1, 10);
            return View(taskList);
        }

        public ActionResult Dashboard()
        {
            var user = (UserdetailDm)Session["SessionData"];
            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }
            var taskStatusCounts = _managerService.GetTaskCounts(user.Id);
            return View(taskStatusCounts);
        }

        public ActionResult CreateTask()
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }
            var employeeList = _managerService.GetEmployeesDetailsByManagerId(user.Id);
            ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(TaskDm taskDm)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            taskDm.TaskStatusId = (long)Enum.Enum.Status.Pending;
            var result = _managerService.AddTask(taskDm, user.Id);
            if (taskDm.Document == null) return RedirectToAction("ListTask");
            var taskDocument = new TaskDocumentDm
            {
                TaskId = result.Id,
                Document = taskDm.Document,
                TaskTitle = taskDm.Title
            };
            SetDocumentPath(taskDocument, user);
            return RedirectToAction("ListTask");
        }

        [HttpPost]
        public ActionResult UpdateTask(TaskDm taskDm)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }
            var result = _managerService.UpdateTask(taskDm);

            return RedirectToAction("ListTask");
        }

        public ActionResult DeleteTask(int? id)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            var result = _managerService.DeleteTask((long)id);

            return RedirectToAction("ListTask");
        }

        public ActionResult DocumentPartialView(int? id)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            var taskName = _managerService.GetTaskNameByTaskId(id);
            var taskDocument = new TaskDocumentDm
            {
                TaskId = (long)id,
                TaskTitle = taskName
            };
            return PartialView("_AddDocument", taskDocument);
        }

        [HttpPost]
        public ActionResult AddDocumentDetails(TaskDocumentDm taskDocument)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            SetDocumentPath(taskDocument, user);
            return user.RoleId == (long)Enum.Enum.Roles.Employee ?
                RedirectToAction("MyTasks", "Employee")
                : RedirectToAction("ListTask");
        }

        private void SetDocumentPath(TaskDocumentDm taskDocument, UserdetailDm user)
        {
            foreach (var file in taskDocument.Document)
            {
                if (file == null) continue;

                var folderPath = Path.Combine(Server.MapPath("~//TaskDocument//"), taskDocument.TaskTitle);
                var filePath = Path.Combine(Server.MapPath("~//TaskDocument//"), taskDocument.TaskTitle, file.FileName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(filePath);
                taskDocument.DocumentPath = filePath;
                _managerService.AddTaskDocument(taskDocument, user.Id);
            }
        }

        [HttpPost]
        public ActionResult DeleteTaskDocument(TaskDocumentDm taskDocument)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            _managerService.DeleteTaskDocument(taskDocument);

            return RedirectToAction("ListTask");
        }

        public ActionResult EditTask(int? id)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            var employeeList = _managerService.GetEmployeesDetailsByManagerId(user.Id);
            ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");
            var task = _managerService.GetTaskByTaskId(id);
            return View(task);

        }

        public ActionResult ViewTaskDetail(int? id)
        {
            var user = (UserdetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            var taskDetail = _managerService.GetTaskAndTaskDocumentDetailByTaskId((long)id);
            return View(taskDetail);
        }
    }
}