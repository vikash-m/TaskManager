using System.Collections.Generic;
using System.Web.Http;
using TaskManagerDAL.Models;
using TaskManagerDAL.DAL;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeRepository _employeeRepository = new EmployeeRepository();
        // GET: Employee


        [HttpGet, Route("")]
        public List<UserDetail> GetEmployee()
        {
            return _employeeRepository.GetEmployee();
        }

        [HttpGet, Route("task-status")]
        public List<TaskStatu> GetStatusList()
        {
            return _employeeRepository.GetStatusList();
        }

        [HttpPut, Route("task/{id}")]
        public bool UpdateTaskStatus(int id, int status)
        {
            return _employeeRepository.UpdateTaskStatus(id, status);
        }


        //[HttpGet, Route("tasks/count/completed/{assignedTo}")]
        [HttpGet, Route("{employeeId}/tasks/count")]
        public int GetTaskCount(int employeeId, int statusId)
        {
            return _employeeRepository.GetTaskCount(employeeId, statusId);
        }

        [HttpGet, Route("{employeeId}/tasks")]
        public List<Task> GetEmployeeTask(int employeeId)
        {
            return _employeeRepository.GetEmployeeTask(employeeId);
        }

        [HttpGet, Route("{taskId}")]
        public Task GetTaskDetail(int taskId)
        {
            return _employeeRepository.GetTaskDetail(taskId);
        }
    }

}