using PagedList;
using System.IO;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using System.Net.Http;
using System.Configuration;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskManager.Controllers
{
    public class ManagerController : Controller
    {
        static HttpClient client = new HttpClient();
        private static string serviceLayerUrl = ConfigurationManager.AppSettings["serviceLayerUrl"] + "/ManagerService";
        private string urlParameters;

        // GET: Manager
        public async Task<ActionResult> ListTask(int? page)
        {

            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }

                var id = user.Id;
                string URL = serviceLayerUrl + "/GetAllTask";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var taskList = response.Content.ReadAsAsync<List<TaskDm>>().Result.ToPagedList(page ?? 1, 10); ;
                    return View("taskList");
                }
                return null;
            }
            catch
            {
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

                var id = user.Id;
                string URL = serviceLayerUrl + "/GetTaskCounts";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var taskStatusCounts = response.Content.ReadAsAsync<TaskStatusCountDm>().Result;
                    return View("taskStatusCounts");
                }
                return null;
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

                var id = user.Id;
                string URL = serviceLayerUrl + "/GetEmployeesDetailsByManagerId";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var employeeList = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");
                    return View();
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(TaskDm taskDm)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (ModelState.IsValid)
                {
                    if (null == user)
                    {
                        return RedirectToAction("Login", "Login");
                    }

                    taskDm.TaskStatusId = (int)EnumClass.Status.Pending;
                    string URL = serviceLayerUrl + "/AddTask";
                    HttpClient client = new HttpClient();
                    urlParameters = "?taskDm=" + taskDm + "&id=" + user.Id;
                    client.BaseAddress = new Uri(URL);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsAsync<TaskDm>().Result;
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

                    string uRLEmployeeList = serviceLayerUrl + "/GetEmployeesDetailsByManagerId";
                    urlParameters = "?ManagerId=" + user.Id;
                    client.BaseAddress = new Uri(uRLEmployeeList);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage responseEmployeeList = await client.GetAsync(urlParameters);
                    if (responseEmployeeList.IsSuccessStatusCode)
                    {
                        var employeeList = responseEmployeeList.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                        ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");
                        return View(taskDm);
                    }
                }
                return null;
            }
            catch
            {
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<ActionResult> EditTask(TaskDm taskDm)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (ModelState.IsValid)
                {
                    if (null == user)
                    {
                        return RedirectToAction("Login", "Login");
                    }

                    taskDm.TaskStatusId = (int)Enum.Enum.Status.Pending;
                    string URL = serviceLayerUrl + "/UpdateTask";
                    HttpClient client = new HttpClient();
                    urlParameters = "?taskDm=" + taskDm;
                    client.BaseAddress = new Uri(URL);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsAsync<TaskDm>().Result;
                        if (taskDm.Document == null) return RedirectToAction("ListTask");
                        var taskDocument = new TaskDocumentDm
                        {
                            TaskId = taskDm.Id,
                            Document = taskDm.Document,
                            TaskTitle = taskDm.Title
                        };
                        SetDocumentPath(taskDocument, user);
                        return RedirectToAction("ListTask");
                    }

                    string uRLEmployeeList = serviceLayerUrl + "/GetEmployeesDetailsByManagerId";
                    urlParameters = "?ManagerId=" + user.Id;
                    client.BaseAddress = new Uri(uRLEmployeeList);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage responseEmployeeList = await client.GetAsync(urlParameters);
                    if (responseEmployeeList.IsSuccessStatusCode)
                    {
                        var employeeList = responseEmployeeList.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                        ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");
                        return View(taskDm);
                    }
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }


        public async Task<ActionResult> DeleteTask(int? id)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }

                string URL = serviceLayerUrl + "/DeleteTask";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListTask");
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> DocumentPartialView(int? id)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }
                if (id == null) return View("ListTask");

                string URL = serviceLayerUrl + "/GetTaskNameByTaskId";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var taskName = response.Content.ReadAsAsync<string>().Result;
                    var taskDocument = new TaskDocumentDm
                    {
                        TaskId = (int)id,
                        TaskTitle = taskName
                    };
                    return PartialView("_AddDocument", taskDocument);
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddDocumentDetails(TaskDocumentDm taskDocument)
        {
            var user = (UserDetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            SetDocumentPath(taskDocument, user);
            return user.RoleId == (long)Enum.Enum.Roles.Employee ?
                RedirectToAction("MyTasks", "Employee")
                : RedirectToAction("ListTask");
        }

        private async void SetDocumentPath(TaskDocumentDm taskDocument, UserDetailDm user)
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
                string URL = serviceLayerUrl + "/AddTaskDocument";
                HttpClient client = new HttpClient();
                urlParameters = "?taskDocument=" + taskDocument + "&Id=" + user.Id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTaskDocument(TaskDocumentDm taskDocument)
        {
            var user = (UserDetailDm)Session["SessionData"];

            if (null == user)
            {
                return RedirectToAction("Login", "Login");
            }

            string URL = serviceLayerUrl + "/DeleteTaskDocument";
            HttpClient client = new HttpClient();
            urlParameters = "?taskDocument=" + taskDocument;
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);

            return RedirectToAction("ListTask");
        }

        public async Task<ActionResult> EditTask(int? id)
        {

            try
            {

                var user = (UserDetailDm)Session["SessionData"];

                if (null == user)
                {
                    return RedirectToAction("Login", "Login");
                }


                string URL = serviceLayerUrl + "/GetEmployeesDetailsByManagerId";
                HttpClient client = new HttpClient();
                urlParameters = "?ManagerId=" + user.Id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var employeeList = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    ViewBag.Employee = new SelectList(employeeList, "Id", "FirstName", "LastName");
                }

                string uRLTask = serviceLayerUrl + "/GetTaskByTaskId";
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(uRLTask);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage responseTask = await client.GetAsync(urlParameters);
                if (responseTask.IsSuccessStatusCode)
                {
                    var task = responseTask.Content.ReadAsAsync<TaskDm>().Result;
                    return View(task);
                }

                return null;
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> CheckForTaskTitleDuplication(string title)
        {

            string URL = serviceLayerUrl + "/GetTaskNames";
            HttpClient client = new HttpClient();
            urlParameters = "?title=" + title;
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);

            var data = response.Content.ReadAsAsync<bool>().Result;

            return data == false ? Json("Sorry, this name already exists", JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}