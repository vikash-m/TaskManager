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
        private readonly ManagerController _managerServiceController = new ManagerController();

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
                foreach (var item in employeeTask)
                {
                    ManagerController managerSercviceController = new ManagerController();
                    item.CreatedByName = await managerSercviceController.GetEmployeeNameById(item.CreatedBy);
                    item.AssignedToName = await managerSercviceController.GetEmployeeNameById(item.AssignedTo);
                    item.TaskStatus = await managerSercviceController.GetTaskStatusNameByTaskStatusId(item.TaskStatusId);
                }
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

        [HttpGet, Route("UpdateTask")]
        public async Task<bool> UpdateTask(int Id, int status)
        {
            var updateStatus = new bool();
            try
            {
                
                string URL = DalLayerUrl + "/employees/UpdateTask";
                HttpClient client = new HttpClient();
                string urlParameters = "?Id=" + Id + "&status=" + status;
                client.BaseAddress = new Uri(URL);
                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);

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
        public async Task<TaskDm> GetTaskDetails(string taskId)
        {
            var taskDetail = new TaskDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/employees/{taskId}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskDetail = response.Content.ReadAsAsync<TaskDm>().Result;
                taskDetail.AssignedToName = await _managerServiceController.GetEmployeeNameById(taskDetail.AssignedTo);
                taskDetail.CreatedByName = await _managerServiceController.GetEmployeeNameById(taskDetail.CreatedBy);
                taskDetail.TaskStatus =
                    await _managerServiceController.GetTaskStatusNameByTaskStatusId(taskDetail.TaskStatusId);

            }
            catch (Exception)
            {
                throw;
            }
            return taskDetail;

        }
    }
}
