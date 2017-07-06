using System.IO;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using PagedList;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace TaskManager.Controllers
{
    public class EmployeeController : Controller
    {

        private static readonly string ServiceLayerUrl = ConfigurationManager.AppSettings["serviceLayerUrl"];
        private string _urlParameters;

        public async Task<ActionResult> Dashboard()
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");

                var url = ServiceLayerUrl + "/GetTaskCounts";
                var client = new HttpClient { BaseAddress = new Uri(url) };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync($"/employees/{user.Id}/tasks/count");
                var taskStatusCountDataModel = new TaskStatusCountDm();
                if (response.IsSuccessStatusCode)
                {
                    taskStatusCountDataModel = response.Content.ReadAsAsync<TaskStatusCountDm>().Result;

                }
                return View(taskStatusCountDataModel);
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> MyTasks(int? page)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");

                var url = ServiceLayerUrl + "/GetEmployeeTasks";
                var client = new HttpClient { BaseAddress = new Uri(url) };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync($"/employees/{user.Id}/tasks");
                var taskList = response.Content.ReadAsAsync<List<TaskDm>>().Result.ToPagedList(page ?? 1, 5);
                return View(taskList);
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<JsonResult> GetStatusList()
        {
            try
            {
                var url = ServiceLayerUrl;
                var client = new HttpClient { BaseAddress = new Uri(url) };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync("/employees/status");
                var statusList = response.Content.ReadAsAsync<List<TaskStatusModel>>().Result;
                return Json(new { data = statusList }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> UpdateTask(string id, int status)
        {
            try
            {
                var url = ServiceLayerUrl + "/employees/UpdateTask";
                var client = new HttpClient();
                _urlParameters = "?id=" + id + "&status=" + status;
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(_urlParameters);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTaskDetails(string id)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");


                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

                // List data response.
                var response = await client.GetAsync($"employees/tasks/{id}");
                var task = response.Content.ReadAsAsync<TaskDm>().Result;
                return View(task);

            }
            catch
            {
                return View("Error");
            }
        }


        public FileResult Download(string fileName)
        {
            return File(fileName, "application/force-download", Path.GetFileName(fileName));
        }
    }
}