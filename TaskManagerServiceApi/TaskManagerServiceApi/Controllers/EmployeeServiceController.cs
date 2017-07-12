using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using TaskDomain.DomainModel;
using TaskManagerServiceApi.Content.Resources;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeeServiceController : ApiController
    {
        // GET: EmployeeService
        private static readonly string DalLayerUrl = DALLayerLinkResources.DalLayerUrl;
        private readonly ManagerController _managerServiceController = new ManagerController();
        Logger logger = LogManager.GetCurrentClassLogger(); 
        [HttpGet, Route("")]
        public async Task<List<UserDetailDm>> GetEmployees()
        {
            var employee = new List<UserDetailDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(DALLayerLinkResources.getEmployeesUrl);
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    employee = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return employee;
        }
        [HttpGet, Route("{employeeId}/tasks")]
        public async Task<List<TaskDm>> GetEmployeeTasks(string employeeId)
        {
            var employeeTask = new List<TaskDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getEmployeeTasksUrl,employeeId));

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    employeeTask = response.Content.ReadAsAsync<List<TaskDm>>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return employeeTask;
        }
        [HttpGet, Route("status")]
        public async Task<List<TaskStatuDm>> GetStatusList()
        {
            var taskStatus = new List<TaskStatuDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(DALLayerLinkResources.getStatusListUrl);

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskStatus = response.Content.ReadAsAsync<List<TaskStatuDm>>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return taskStatus;
        }
        [HttpGet, Route("{employeeId}/tasks/count")]
        public async Task<TaskStatusCountDm> GetTaskCounts(string employeeId)
        {
            var taskCount = new TaskStatusCountDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var pendingCount = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskCountUrl, employeeId, Convert.ToInt32(EnumClass.Status.Pending)));
                var inProgressCount = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskCountUrl, employeeId, Convert.ToInt32(EnumClass.Status.InProgress)));
                var completedCount = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskCountUrl, employeeId, Convert.ToInt32(EnumClass.Status.Completed)));
                if (pendingCount.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskCount.Pending = pendingCount.Content.ReadAsAsync<int>().Result;
                if (inProgressCount.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskCount.InProgress = inProgressCount.Content.ReadAsAsync<int>().Result;
                if (completedCount.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskCount.Completed = completedCount.Content.ReadAsAsync<int>().Result;
                taskCount.Total = taskCount.Pending + taskCount.InProgress + taskCount.Completed;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return taskCount;
        }
        [HttpGet, Route("UpdateTask")]
        public async Task<bool> UpdateTask(string Id, int status)
        {
            var updateStatus = new bool();
            try
            { 
                string URL = DalLayerUrl + DALLayerLinkResources.updateTaskUrl;
                HttpClient client = new HttpClient();
                string urlParameters = "?Id=" + Id + "&status=" + status;
                client.BaseAddress = new Uri(URL);
                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    updateStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
            }
            return updateStatus;
        }
        [HttpGet, Route("tasks/{taskId}")]
        public async Task<TaskDm> GetTaskDetails(string taskId)
        {
            var taskDetail = new TaskDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskDetailsUrl,taskId));

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskDetail = response.Content.ReadAsAsync<TaskDm>().Result;
               

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return taskDetail;

        }
    }
}
