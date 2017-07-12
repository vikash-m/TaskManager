using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NLog;
using PagedList;
using TaskDomain.DomainModel;
using TaskManager.Content.Resources;

namespace TaskManager.Controllers
{
    public class ManagerController : Controller
    {
        private static readonly string ServiceLayerUrl = ServiceLayerLinkResource.serviceLayerUrl;
        Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<ActionResult> ListTask(int? page)
        {

            try
            {

                if (!ModelState.IsValid)
                    return RedirectToAction("Login", "Login");

                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }

                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                // List data response.
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.listTaskUrl , user.Id));

                if (!response.IsSuccessStatusCode) return View();
                var taskList = response.Content.ReadAsAsync<List<TaskDm>>().Result.ToPagedList(page ?? 1, 10);
                return View(taskList);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }



        }

        public async Task<ActionResult> Dashboard()
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }

                var employeeId = user.Id;
                var taskStatusCounts = new TaskStatusCountDm();
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                // List data response.
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.dashboardUrl , employeeId));
                if (response.IsSuccessStatusCode)
                    taskStatusCounts = response.Content.ReadAsAsync<TaskStatusCountDm>().Result;
                return View(taskStatusCounts);


            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> CreateTask()
        {

            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }

                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                // List data response.
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.createTaskUrl, user.Id));
                var employeeList = new List<UserDetailDm>();
                if (response.IsSuccessStatusCode)
                    employeeList = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;

                ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName");
                return View();


            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(TaskDm taskDm)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                if (ModelState.IsValid)
                {
                    if (null == user)
                    {
                        return RedirectToAction("Login", "Login");
                    }
                    var taskDocument = new TaskDocumentDm();
                    taskDm.TaskStatusId = (int)EnumClass.Status.Pending;
                    if (taskDm.Document.Count > 0 || taskDocument.Document.Contains(null))
                    {
                        taskDocument.Document = new List<HttpPostedFileBase>(taskDm.Document);
                        taskDocument.TaskTitle = taskDm.Title;

                    }

                    // foreach (var document in taskDm.)
                    taskDm.Document.Clear(); // List data response.
                    var response = await client.PostAsJsonAsync(string.Format(ServiceLayerLinkResource.createTaskManagerUrl , user.Id), taskDm);

                    if (response.IsSuccessStatusCode)
                    {
                        var task = response.Content.ReadAsAsync<TaskDm>().Result;
                        taskDocument.TaskId = task.Id;
                        if (!taskDocument.Document.Contains(null))
                            await SetDocumentPathAndSaveFile(taskDocument);
                        return RedirectToAction("ListTask");
                    }
                }
                // List data response.
                var employeeList = new List<UserDetailDm>();
                var responseEmployeeList = await client.GetAsync(string.Format(ServiceLayerLinkResource.createTaskLinkUrl, user.Id));
                if (responseEmployeeList.IsSuccessStatusCode)

                    employeeList = responseEmployeeList.Content.ReadAsAsync<List<UserDetailDm>>().Result;

                ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");

                return View(taskDm);

            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<ActionResult> EditTask(TaskDm taskDm)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                if (ModelState.IsValid)
                {
                    if (null == user)
                    {
                        return RedirectToAction("Login", "Login");
                    }
                    var taskDocument = new TaskDocumentDm();

                    if (taskDm.Document.Count > 0 || taskDocument.Document.Contains(null))
                    {
                        taskDocument.Document = new List<HttpPostedFileBase>(taskDm.Document);
                        taskDocument.TaskTitle = taskDm.Title;
                        taskDocument.ModifiedBy = user.Id;
                        taskDocument.Id = Guid.NewGuid().ToString();
                        taskDocument.ModifiedDate = DateTime.Now;
                        taskDocument.AddedBy = user.Id;

                    }
                    taskDm.Document.Clear();
                    // List data response.
                    var response = await client.PutAsJsonAsync(string.Format(ServiceLayerLinkResource.editTaskUrl , taskDm.Id , user.Id), taskDm);
                    if (response.IsSuccessStatusCode)
                    {
                        if (!taskDocument.Document.Contains(null))
                            await SetDocumentPathAndSaveFile(taskDocument);
                        return RedirectToAction("ListTask");
                    }
                }

                // List data response.
                var employeeList = new List<UserDetailDm>();
                var responseEmployeeList = await client.GetAsync(string.Format(ServiceLayerLinkResource.createTaskLinkUrl , user.Id));
                if (responseEmployeeList.IsSuccessStatusCode)

                    employeeList = responseEmployeeList.Content.ReadAsAsync<List<UserDetailDm>>().Result;

                ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");

                return View(taskDm);


            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }

        }

        public async Task<ActionResult> DeleteTask(string id)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];


                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }

                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

                // List data response.
                var response = await client.DeleteAsync(string.Format(ServiceLayerLinkResource.deleteTaskUrl , id , user.Id));

            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }

            return RedirectToAction("ListTask");
        }

        public async Task<ActionResult> DocumentPartialView(string id)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }
                if (id == null) return View("ListTask");


                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

                // List data response.
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.documentPartialViewUrl , id));
                if (!response.IsSuccessStatusCode) return View("Error");
                var taskName = response.Content.ReadAsAsync<string>().Result;
                var taskDocument = new TaskDocumentDm
                {
                    TaskId = id,
                    TaskTitle = taskName
                };
                return PartialView("_AddDocument", taskDocument);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDocumentDetails(TaskDocumentDm taskDocument)
        {
            var user = (UserDetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            var response = await SetDocumentPathAndSaveFile(taskDocument);

            return response && user.RoleId == (int)Enum.Enum.Roles.Employee ?
                RedirectToAction("MyTasks", "Employee")
              : RedirectToAction("ListTask", "Manager");
        }

        private async Task<bool> SetDocumentPathAndSaveFile(TaskDocumentDm taskDocument)
        {
            var user = (UserDetailDm)Session["SessionData"];
            var uploadStatus = new bool();
            var document = new List<HttpPostedFileBase>(taskDocument.Document);
            taskDocument.Document.Clear();
            foreach (var file in document)
            {
                if (file == null) return true;
                taskDocument.TaskTitle = taskDocument.TaskTitle.Replace(" ", "_");
                var fileName = file.FileName.Replace(" ", "_");
                var folderPath = Path.Combine(Server.MapPath("~//TaskDocument//"), taskDocument.TaskTitle);
                var filePath = Path.Combine(Server.MapPath("~//TaskDocument//"), taskDocument.TaskTitle, fileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(filePath);
                taskDocument.DocumentPath = filePath;
                taskDocument.Id = Guid.NewGuid().ToString();
                taskDocument.CreateDate = DateTime.Now;
                taskDocument.AddedBy = user.Id;
                uploadStatus = await AddDocument(taskDocument);
            }


            return uploadStatus;
        }

        private async Task<bool> AddDocument(TaskDocumentDm taskDocumentDm)
        {
            var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
            var documentUploadStatus = new bool(); ;
            // List data response.
            var response = await client.PostAsJsonAsync(ServiceLayerLinkResource.addDocumentUrl, taskDocumentDm);
            if (response.IsSuccessStatusCode)
                documentUploadStatus = response.Content.ReadAsAsync<bool>().Result;
            return documentUploadStatus;

        }

        public async Task<ActionResult> EditTask(string id)
        {

            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }

                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };


                // List data response.
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.editTaskLinkUrl , id));
                var task = new TaskDm();
                // List data response.
                if (response.IsSuccessStatusCode)
                    task = response.Content.ReadAsAsync<TaskDm>().Result;
                var employeeList = new List<UserDetailDm>();
                var responseEmployeeList = await client.GetAsync(string.Format(ServiceLayerLinkResource.createTaskLinkUrl, user.Id));
                if (responseEmployeeList.IsSuccessStatusCode)

                    employeeList = responseEmployeeList.Content.ReadAsAsync<List<UserDetailDm>>().Result;

                ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");

                return View(task);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }

        public async Task<ActionResult> CheckForTaskName(string title)
        {
            var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

            var result = new bool();
            // List data response.
            var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.checkForTaskNameUrl , title));
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<bool>().Result;
            }
            return result == false ? Json(ServiceLayerLinkResource.taskExistsError, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}