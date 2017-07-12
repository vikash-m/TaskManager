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
    [RoutePrefix("manager")]
    public class ManagerController : ApiController
    {
        private static readonly string DalLayerUrl = DALLayerLinkResources.DalLayerUrl;
        Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet, Route("{managerId}/employees")]
        public async Task<List<UserDetailDm>> GetEmployeesDetailsByManagerId(string managerId)
        {
            var employee = new List<UserDetailDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getEmployeesDetailsByManagerIdUrl, managerId));
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
                var response = await client.PostAsJsonAsync(DALLayerLinkResources.createTaskUrl, task);
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskResult = response.Content.ReadAsAsync<TaskDm>().Result;
                var taskAssignmentList = new List<TaskAssignmentDm>();
                foreach (var assignTo in task.AssignedTo)
                {
                    var taskAssignment = new TaskAssignmentDm()
                    {
                        Id = Guid.NewGuid().ToString(),
                        AssignedTo = assignTo,
                        AssignedBy = task.CreatedBy,
                        AssignedOn = DateTime.Now,
                        TaskId = task.Id

                    };
                    taskAssignmentList.Add(taskAssignment);
                }
                var assignToResponse = await client.PostAsJsonAsync(DALLayerLinkResources.addAssignTo, taskAssignmentList);
                if (assignToResponse.IsSuccessStatusCode)
                {
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
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
                var response = await client.PostAsJsonAsync(DALLayerLinkResources.addTaskDocumentUrl, taskDocument);
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    documentUploadStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
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
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getAllTaskUrl, managerId));
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    tasks = response.Content.ReadAsAsync<List<TaskDm>>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
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
                var response = await client.PutAsJsonAsync(DALLayerLinkResources.updateTaskLinkUrl, task);
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskUpdateStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
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
                var response = await client.DeleteAsync(string.Format(DALLayerLinkResources.deleteTaskUrl, id, loginUser));
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskDeleteStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
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
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskNameByTaskIdUrl, taskId));
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    taskName = response.Content.ReadAsAsync<string>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return taskName;
        }

        [HttpGet, Route("tasks/{taskId}")]
        public async Task<TaskDm> GetTaskByTaskId(string taskId)
        {
            var task = new TaskDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskByTaskIdUrl, taskId));
                if (response.IsSuccessStatusCode)
                    task = response.Content.ReadAsAsync<TaskDm>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
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
                var pendingCount = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskCountsManagerUrl, employeeId, Convert.ToInt32(EnumClass.Status.Pending)));
                var inProgressCount = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskCountsManagerUrl, employeeId, Convert.ToInt32(EnumClass.Status.InProgress)));
                var completedCount = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskCountsManagerUrl, employeeId, Convert.ToInt32(EnumClass.Status.Completed)));

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

        [HttpGet, Route("tasks/task/{title}")]
        public async Task<bool> CheckForTaskName(string title)
        {
            var taskNameExist = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.checkForTaskNameUrl, title));
                if (response.IsSuccessStatusCode)
                {
                    taskNameExist = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
            }
            return taskNameExist;
        }

        [HttpGet, Route("tasks/{id}/name")]
        public async Task<string> GetEmployeeNameById(string id)
        {
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var createdByResponse = await client.GetAsync(string.Format(DALLayerLinkResources.getEmployeeNameByIdUrl, id));
            return createdByResponse.Content.ReadAsAsync<string>().Result;
        }

        public async Task<string> GetTaskStatusNameByTaskStatusId(int id)
        {
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var statusResponse = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskStatusNameByTaskStatusIdUrl, id));
            return statusResponse.Content.ReadAsAsync<string>().Result;
        }

        [HttpGet, Route("tasks/{taskId}/task-document")]
        public async Task<List<TaskDocumentDm>> GetTaskDocumentBytaskId(string taskId)
        {
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var taskDocumentList = await client.GetAsync(string.Format(DALLayerLinkResources.getTaskDocumentBytaskIdUrl, taskId));
            return taskDocumentList.Content.ReadAsAsync<List<TaskDocumentDm>>().Result;
        }
    }
}