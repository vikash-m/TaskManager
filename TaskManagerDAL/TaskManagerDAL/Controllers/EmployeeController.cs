using System.Collections.Generic;
using System.Web.Http;
using TaskManagerDAL.Models;
using TaskManagerDAL.DAL;
using TaskDomain.DomainModel;
using TaskDomain.DomainModel.CustomExceptions;
using System;
using NLog;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeRepository _employeeRepository = new EmployeeRepository();
        Logger logger = LogManager.GetCurrentClassLogger();
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
            try
            {
                try
                {
                    return _employeeRepository.UpdateTaskStatus(id, status);
                }
                catch
                {
                    throw new UpdateTaskStatusException("Error at UpdateTaskStatus method of Employee controller in DAL layer.");
                }
            }
            catch (UpdateTaskStatusException updateTaskStatusException)
            {
                logger.Error(updateTaskStatusException, updateTaskStatusException.Message);
                return false;
            }
        }
        
        [HttpGet, Route("{employeeId}/tasks/count")]
        public int? GetTaskCount(string employeeId, int statusId)
        {
            try
            { 
               try
               {
                    return _employeeRepository.GetTaskCount(employeeId, statusId);
               }
               catch
               {
                   throw new DashboardTaskCountException("Error at GetTaskCount method of Employee controller in DAL layer.");
               }
            }
            catch(DashboardTaskCountException dashboardTaskCountException)
            {
                logger.Error(dashboardTaskCountException, dashboardTaskCountException.Message);
                return null;
            }
        }

        [HttpGet, Route("{employeeId}/tasks")]
        public List<TaskDm> GetEmployeeTask(string employeeId)
        {
            try
            {
                try
                {

                    return _employeeRepository.GetEmployeeTask(employeeId);
                }
                catch
                {
                    throw new EmployeeTaskException("Error at GetEmployeeTask method of Employee controller in DAL layer.");
                }
            }
            catch (EmployeeTaskException employeeTaskException)
            {
                logger.Error(employeeTaskException, employeeTaskException.Message);
                return null;
            }
            
        }

        [HttpGet, Route("{taskId}")]
        public TaskDm GetTaskDetail(string taskId)
        {
            try
            {
               try
               {
               
                  return _employeeRepository.GetTaskDetail(taskId);
               }
               catch
               {
                throw new TaskDetailException("Error at GetTaskDetail method of Employee controller in DAL layer.");
               }
            }
            catch (TaskDetailException taskDetailException)
            {
                logger.Error(taskDetailException, taskDetailException.Message);
                return null;
            }
        }
    }

}