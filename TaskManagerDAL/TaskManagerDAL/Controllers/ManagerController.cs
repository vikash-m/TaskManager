using System.Collections.Generic;
using System.Web.Http;
using TaskManagerDAL.DAL;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("manager")]
    public class ManagerController : ApiController
    {
        private readonly ManagerRepository _managerRepository = new ManagerRepository();

        [HttpGet, Route("employees")]
        public List<UserDetail> GetEmployeesDetailsByManagerId(string managerId)
        {
            return _managerRepository.GetEmployeesDetailsByManagerId(managerId);
        }

        [HttpPost, Route("task")]
        public Task CreateTask(Task task)
        {
            return _managerRepository.CreateTask(task);
        }

        [HttpGet, Route("{managerId}/tasks")]
        public List<Task> GetAllTask(string managerId)
        {
            return _managerRepository.GetAllTask(managerId);
        }

        [HttpGet, Route("status/{taskId}")]
        public string GetTaskStatusByTaskStatusId(int taskId)
        {
            return _managerRepository.GetTaskStatusByTaskStatusId(taskId);
        }

        [HttpGet, Route("employeename/{id}")]
        public string GetEmployeeNameById(string id)
        {
            return _managerRepository.GetEmployeeNameById(id);
        }

        [HttpPut, Route("task")]
        public bool UpdateTask(Task task)
        {
            return _managerRepository.UpdateTask(task);
        }

        [HttpDelete, Route("{id}")]
        public bool DeleteTask(int id)
        {
            return _managerRepository.DeleteTask(id);
        }

        [HttpGet, Route("tasks/{id}/task-name")]
        public string GetTaskNameByTaskId(string id)
        {
            return _managerRepository.GetTaskNameByTaskId(id);
        }

        [HttpPost, Route("document")]
        public bool AddTaskDocument(TaskDocument taskDocument)
        {
            return _managerRepository.AddTaskDocument(taskDocument);
        }

        [HttpDelete, Route("document")]
        public bool DeleteTaskDocument(TaskDocument taskDocument)
        {
            return _managerRepository.DeleteTaskDocument(taskDocument);
        }

        [HttpGet, Route("tasks/{id}")]
        public Task GetTaskByTaskId(string id)
        {
            return _managerRepository.GetTaskByTaskId(id);
        }

        [HttpGet, Route("tasks/tasks/{title}")]
        public bool CheckTaskNames(string title)
        {
            return _managerRepository.CheckForTaskName(title);
        }

        [HttpGet, Route("{employeeId}/tasks/count")]
        public int GetTaskCounts(string employeeId, int statusId)
        {
            return _managerRepository.GetTaskCounts(employeeId, statusId);
        }




    }
}
