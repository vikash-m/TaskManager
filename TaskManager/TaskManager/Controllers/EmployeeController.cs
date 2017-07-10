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
using NLog;
using TaskDomain.CustomExceptions;
using TaskManager.Content.Resources;

namespace TaskManager.Controllers
{
    public class EmployeeController : Controller
    {

        
        private static readonly string ServiceLayerUrl = ServiceLayerLinkResource.serviceLayerUrl;

        private string _urlParameters;
        Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<ActionResult> Dashboard()
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");

                
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.getTaskCountsUrl, user.Id));
                var taskStatusCountDataModel = new TaskStatusCountDm();
                if (response.IsSuccessStatusCode)
                {
                    taskStatusCountDataModel = response.Content.ReadAsAsync<TaskStatusCountDm>().Result;

                }
                return View(taskStatusCountDataModel);
            }
            catch (DashboardTaskCountException dashboardTaskCountException)
            {
                logger.Error(dashboardTaskCountException, dashboardTaskCountException.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }

        public async Task<ActionResult> MyTasks(int? page)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");

                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.getEmployeeTasksUrl, user.Id));
                var taskList = response.Content.ReadAsAsync<List<TaskDm>>().Result.ToPagedList(page ?? 1, 5);
                return View(taskList);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }

        public async Task<JsonResult> GetStatusList()
        {
            try
            {
                
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(ServiceLayerLinkResource.getStatusListUrl);
                var statusList = response.Content.ReadAsAsync<List<TaskStatusModel>>().Result;
                return Json(new { data = statusList }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> UpdateTask(string id, int status)
        {
            try
            {
                var url = ServiceLayerUrl + ServiceLayerLinkResource.updateTaskUrl;
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
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
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
                var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.getTaskDetailsUrl, id));
                var task = response.Content.ReadAsAsync<TaskDm>().Result;
                return View(task);

            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }


        public FileResult Download(string fileName)
        {
            return File(fileName, "application/force-download", Path.GetFileName(fileName));
        }
    }
}