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

        private static string serviceLayerUrl = ConfigurationManager.AppSettings["serviceLayerUrl"];
        private string urlParameters;

        public async Task<ActionResult> Dashboard()
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");

                string URL = serviceLayerUrl + "/GetTaskCounts";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync($"/employees/{user.Id}/tasks/count");
                var taskStatusCountDataModel = new TaskStatusCountDm();
                if (response.IsSuccessStatusCode)
                {
                    taskStatusCountDataModel = response.Content.ReadAsAsync<TaskStatusCountDm>().Result;

                }
                return View(taskStatusCountDataModel);
            }
            catch(Exception)
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

                string URL = serviceLayerUrl + "/GetEmployeeTasks";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync($"/employees/{user.Id}/tasks");
                var taskList = response.Content.ReadAsAsync<List<TaskDm>>().Result.ToPagedList(page ?? 1, 5); ;
                return View(taskList);
            }
            catch(Exception)
            {
                return View("Error");
            }
        }
        
        public async Task<JsonResult> GetStatusList()
        {
            try
            {
                string URL = serviceLayerUrl;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync("/employees/status");
                var statusList = response.Content.ReadAsAsync<List<TaskStatusModel>>().Result;
                return Json(new { data = statusList }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception)
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> UpdateTask(int id, int status)
        {
            try
            {
                string URL = serviceLayerUrl + "/employees/UpdateTask";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id + "&status=" + status;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch(Exception)
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


                var client = new HttpClient { BaseAddress = new Uri(serviceLayerUrl) };

                // List data response.
                var response = await client.GetAsync($"employees/tasks/{id}");
                var task = response.Content.ReadAsAsync<TaskDm>().Result;
                return View(task);
               
            }
            catch(Exception)
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