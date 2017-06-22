using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskDomain.DomainModel;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeeServiceController : ApiController
    {
        // GET: EmployeeService
        private static readonly string DalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"];

        [HttpGet, Route("")]
        public async Task<List<UserDetailDm>> GetEmployees()
        {
            var employee = new List<UserDetailDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync("/employees");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    employee = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return employee;
        }

        [HttpGet, Route("{employeeId}/tasks")]
        public async Task<List<TaskDm>> GetEmployeeTasks(int employeeId)
        {
            var employeeTask = new List<TaskDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/employees/{employeeId}/tasks");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    employeeTask = response.Content.ReadAsAsync<List<TaskDm>>().Result;


            }
            catch (Exception)
            {
                throw;
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
                var response = await client.GetAsync("/employees/task-status");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskStatus = response.Content.ReadAsAsync<List<TaskStatuDm>>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return taskStatus;
        }

        [HttpGet, Route("{employeeId}/tasks/count")]
        public async Task<TaskStatusCountDm> GetTaskCounts(int employeeId)
        {
            var taskCount = new TaskStatusCountDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var pendingCount = await client.GetAsync($"/employees/{employeeId}/tasks/count/?statusId={Convert.ToInt32(EnumClass.Status.Pending)}");
                var inProgressCount = await client.GetAsync($"/employees/{employeeId}/tasks/count/?statusId={Convert.ToInt32(EnumClass.Status.InProgress)}");
                var completedCount = await client.GetAsync($"/employees/{employeeId}/tasks/count/?statusId={Convert.ToInt32(EnumClass.Status.Completed)}");

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
            catch (Exception)
            {
                throw;
            }
            return taskCount;
        }

        [HttpPost, Route("{employeeId}")]
        public async Task<bool> UpdateTask(int employeeId, int status)
        {
            var updateStatus = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PostAsJsonAsync($"/employees/{employeeId}", status);

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    updateStatus = response.Content.ReadAsAsync<bool>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return updateStatus;
        }

        [HttpGet, Route("tasks/{taskId}")]
        public async Task<TaskDm> GetTaskDetails(int taskId)
        {
            var taskDetail = new TaskDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/employees/{taskId}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskDetail = response.Content.ReadAsAsync<TaskDm>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return taskDetail;

        }

        [HttpGet, Route("{id}/name")]
        public async Task<string> GetEmployeeNameById(int id)
        {
            var employeeName = string.Empty;
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/manager/{id}/name");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    employeeName = response.Content.ReadAsAsync<string>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return employeeName;
        }
    }
}
