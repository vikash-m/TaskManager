using System.Collections.Generic;
using System.Web.Http;
using TaskManagerDAL.Models;
using TaskManagerDAL.DAL;
using TaskDomain.DomainModel;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeRepository _employeeRepository = new EmployeeRepository();
        
        [HttpGet, Route("")]
        public List<UserDetail> GetEmployee()
        {
            return _employeeRepository.GetEmployee();
        }

        [HttpGet, Route("task-status")]
        public List<TaskStatuDm> GetStatusList()
        {
            return _employeeRepository.GetStatusList();
        }

        [HttpGet, Route("UpdateTask")]
        public bool UpdateTaskStatus(string id, int status)
        {
            return _employeeRepository.UpdateTaskStatus(id, status);
        }
        
        [HttpGet, Route("{employeeId}/tasks/count")]
        public int GetTaskCount(string employeeId, int statusId)
        {
            return _employeeRepository.GetTaskCount(employeeId, statusId);
        }

        [HttpGet, Route("{employeeId}/tasks")]
        public List<TaskDm> GetEmployeeTask(string employeeId)
        {
            return _employeeRepository.GetEmployeeTask(employeeId);
        }

        [HttpGet, Route("{taskId}")]
        public TaskDm GetTaskDetail(string taskId)
        {
            return _employeeRepository.GetTaskDetail(taskId);
        }
    }

}