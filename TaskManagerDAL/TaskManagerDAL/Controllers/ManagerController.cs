using NLog;
using System.Collections.Generic;
using System.Web.Http;
using TaskDomain.DomainModel;
using TaskManagerDAL.CustomException;
using TaskManagerDAL.DAL;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("manager")]
    public class ManagerController : ApiController
    {
        private readonly ManagerRepository _managerRepository = new ManagerRepository();
        Logger logger = LogManager.GetCurrentClassLogger(); 
        [HttpGet, Route("employees")]
        public List<UserDetailDm> GetEmployeesDetailsByManagerId(string managerId)
        {
            try
            {
                try
                {
                    return _managerRepository.GetEmployeesDetailsByManagerId(managerId);
                }
                catch
                {
                    throw new TaskManagerException("Error at Employee Details By Manager method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpPost, Route("task")]
        public Task CreateTask(Task task)
        {
            try {
                try
                {
                    return _managerRepository.CreateTask(task);
                }
                catch
                {
                    throw new TaskManagerException("Error at Create Task method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpGet, Route("{managerId}/tasks")]
        public List<TaskDm> GetAllTask(string managerId)
        {
            try {
                try
                {
                    return _managerRepository.GetAllTask(managerId);
                }
                catch
                {
                    throw new TaskManagerException("Error at Get All Task method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }

        }

        [HttpGet, Route("status/{taskId}")]
        public string GetTaskStatusByTaskStatusId(int? taskId)
        {
            try {
                try
                {
                    return _managerRepository.GetTaskStatusByTaskStatusId(taskId);
                }
                catch
                {
                    throw new TaskManagerException("Error at Task Status By TaskId method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpGet, Route("employeename/{id}")]
        public string GetEmployeeNameById(string id)
        {
            try {
                try
                {
                    return _managerRepository.GetEmployeeNameById(id);
                }
                catch
                {
                    throw new TaskManagerException("Error at EmployeeName by Id method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpPut, Route("task")]
        public bool UpdateTask(Task task)
        {
            try {
                try
                {
                    return _managerRepository.UpdateTask(task);
                }
                catch
                {
                    throw new TaskManagerException("Error at Update Task method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }

        [HttpDelete, Route("{id}")]
        public bool DeleteTask(string id, string loginUser)
        {
            try {
                try
                {
                    return _managerRepository.DeleteTask(id, loginUser);
                }
                catch
                {
                    throw new TaskManagerException("Error at Delete Task method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }

        }

        [HttpGet, Route("tasks/{id}/task-name")]
        public string GetTaskNameByTaskId(string id)
        {
            try {
                try
                {
                    return _managerRepository.GetTaskNameByTaskId(id);
                }
                catch
                {
                    throw new TaskManagerException("Error at Task Name By TaskId method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpPost, Route("document")]
        public bool AddTaskDocument(TaskDocument taskDocument)
        {
            try
            {

                try
                {
                    return _managerRepository.AddTaskDocument(taskDocument);
                }
                catch
                {
                    throw new TaskManagerException("Error at Add Task Document method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }

        [HttpDelete, Route("document")]
        public bool DeleteTaskDocument(TaskDocument taskDocument)
        {
            try {
                try
                {
                    return _managerRepository.DeleteTaskDocument(taskDocument);
                }
                catch
                {
                    throw new TaskManagerException("Error at Delete Task Document method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }

        [HttpGet, Route("tasks/{id}")]
        public TaskDm GetTaskByTaskId(string id)
        {
            try {
                try
                {
                    return _managerRepository.GetTaskByTaskId(id);
                }
                catch
                {
                    throw new TaskManagerException("Error at TAsk by TaskId method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpGet, Route("tasks/tasks/{title}")]
        public bool CheckForTaskName(string title)
        {
            try {
                try
                {
                    return _managerRepository.CheckForTaskName(title);
                }
                catch
                {
                    throw new TaskManagerException("Error at Check for TaskName method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }

        [HttpGet, Route("{employeeId}/tasks/count")]
        public int GetTaskCounts(string employeeId, int statusId)
        {
            try
            {
                try
                {
                    return _managerRepository.GetTaskCounts(employeeId, statusId);
                }
                catch
                {
                    throw new TaskManagerException("Error at Task Count method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return 0;
            }
        }

        [HttpGet, Route("tasks/{taskId}/task-document")]
        public List<TaskDocumentDm> GetTaskDocumentBytaskId(string taskId)
        {
            try
            {
                try
                {
                    return _managerRepository.GetTaskDocumentBytaskId(taskId);
                }
                catch
                {
                    throw new TaskManagerException("Error at Task Document by TaskId method of Employee controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }



    }
}
