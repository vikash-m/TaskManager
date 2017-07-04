using System.Web.Http;
using System.Configuration;
using System.Net.Http;
using TaskDomain.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("manager")]
    public class ManagerController : ApiController
    {
        private static readonly string DalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"];

        [HttpGet, Route("{managerId}/employees")]
        public async Task<List<UserDetailDm>> GetEmployeesDetailsByManagerId(string managerId)
        {
            var employee = new List<UserDetailDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/manager/employees?managerId={managerId}");

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

        [HttpPost, Route("")]
        public async Task<TaskDm> CreateTask(string loginUserId, TaskDm task)
        {
            var taskResult = new TaskDm();
            try
            {
                task.Id = Guid.NewGuid().ToString();
                task.CreateDate = DateTime.Now;
                task.CreatedBy = loginUserId;
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PostAsJsonAsync("/manager/task", task);

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskResult = response.Content.ReadAsAsync<TaskDm>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return taskResult;
        }

        [HttpPost, Route("document")]
        public async Task<bool> AddTaskDocument(TaskDocumentDm taskDocument)
        {
            var documentUploadStatus = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PostAsJsonAsync("/manager/document", taskDocument);

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    documentUploadStatus = response.Content.ReadAsAsync<bool>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return documentUploadStatus;
        }

        [HttpGet, Route("{managerId}/tasks")]
        public async Task<List<TaskDm>> GetAllTask(string managerId)
        {
            var tasks = new List<TaskDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/manager/{managerId}/tasks");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    tasks = response.Content.ReadAsAsync<List<TaskDm>>().Result;

            }
            catch (Exception)
            {
                throw;
            }
            return tasks;
        }

        [HttpPut, Route("{id}")]
        public async Task<bool> UpdateTask(string id, string loginUser, TaskDm task)
        {
            var taskUpdateStatus = new bool();
            try
            {
                task.ModifiedDate = DateTime.Now;
                task.ModifiedBy = id;
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PutAsJsonAsync("/manager/task", task);
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskUpdateStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return taskUpdateStatus;
        }

        [HttpDelete, Route("{id}")]
        public async Task<bool> DeleteTask(string id, string loginUser)
        {
            var taskDeleteStatus = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                //var taskToBeDeleted = await client.GetAsync($"/manager/task/{id}");
                //var task = taskToBeDeleted.Content.ReadAsAsync<TaskDm>();
                //task.Result.ModifiedDate = DateTime.Now;
                //task.Result.ModifiedBy = loginUserDm;
                //task.Result.IsDeleted = true;

                var response = await client.DeleteAsync($"/manager/{id}/?loginUser={loginUser}");
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskDeleteStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return taskDeleteStatus;
        }

        [HttpGet, Route("tasks/{taskId}/task-name")]
        public async Task<string> GetTaskNameByTaskId(string taskId)
        {
            var taskName = string.Empty;
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/manager/tasks/{taskId}/task-name");
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskName = response.Content.ReadAsAsync<string>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return taskName;
        }


        //public async Task<bool> DeleteTaskDocument(TaskDocumentDm taskDocument)
        //{
        //    var taskDeleteStatus = new bool();
        //    try
        //    {
        //        var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
        //        var taskToBeDeleted = await client.GetAsync($"/manager/task/{id}");
        //        var task = taskToBeDeleted.Content.ReadAsAsync<TaskDm>();
        //        task.Result.ModifiedDate = DateTime.Now;
        //        //task.Result.ModifiedBy = loginUserDm.Id;

        //        var response = await client.PostAsJsonAsync($"/manager/document", task);
        //        if (response.IsSuccessStatusCode)
        //            // Parse the response body. Blocking!
        //            taskDeleteStatus = response.Content.ReadAsAsync<bool>().Result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return taskDeleteStatus;
        //}

        [HttpGet, Route("tasks/{taskId}")]
        public async Task<TaskDm> GetTaskByTaskId(string taskId)
        {
            var task = new TaskDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/manager/tasks/{taskId}");
                if (response.IsSuccessStatusCode)
                    task = response.Content.ReadAsAsync<TaskDm>().Result;

            }
            catch (Exception)
            {
                throw;
            }
            return task;
        }

        [HttpGet, Route("{employeeId}/tasks/count")]
        public async Task<TaskStatusCountDm> GetTaskCounts(int employeeId)
        {
            var taskCount = new TaskStatusCountDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var pendingCount = await client.GetAsync($"/manager/{employeeId}/tasks/count/?statusId={Convert.ToInt32(EnumClass.Status.Pending)}");
                var inProgressCount = await client.GetAsync($"/manager/{employeeId}/tasks/count/?statusId={Convert.ToInt32(EnumClass.Status.InProgress)}");
                var completedCount = await client.GetAsync($"/manager/{employeeId}/tasks/count/?statusId={Convert.ToInt32(EnumClass.Status.Completed)}");

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
                throw;
            }
            return taskCount;
        }

        [HttpGet, Route("tasks/task/{title}")]
        public async Task<bool> CheckForTaskName(string title)
        {
            var taskNameExist = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/manager/tasks/tasks/{title}");
                if (response.IsSuccessStatusCode)
                {
                    taskNameExist = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return taskNameExist;
        }

        [HttpGet, Route("tasks/{id}/name")]
        public async Task<string> GetEmployeeNameById(string id)
        {
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var createdByResponse = await client.GetAsync($"/manager/employeename/{id}");
            return createdByResponse.Content.ReadAsAsync<string>().Result;

        }

        public async Task<string> GetTaskStatusNameByTaskStatusId(int id)
        {
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var statusResponse = await client.GetAsync($"/manager/status/{id}");
            return statusResponse.Content.ReadAsAsync<string>().Result;
        }

        [HttpGet, Route("tasks/{taskId}/task-document")]
        public async Task<List<TaskDocumentDm>> GetTaskDocumentBytaskId(string taskId)
        {
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var taskDocumentList = await client.GetAsync($"/manager/tasks/{taskId}/task-document");

            return taskDocumentList.Content.ReadAsAsync<List<TaskDocumentDm>>().Result;




        }
    }
}