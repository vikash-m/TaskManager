using System.Collections.Generic;
using TaskDomain.DomainModel;
using TaskDAL.Repository;

namespace TaskServiceLayer
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository = new EmployeeRepository();
        public List<EmployeeModelDm> GetEmployees()
        {
            var employeeList = _employeeRepository.GetEmployee();
            return employeeList;
        }

        public List<TaskDm> GetEmployeeTasks(long id)
        {
            var employeeTasks = _employeeRepository.GetEmployeeTasks(id);
            return employeeTasks;
        }

        public List<TaskStatuDm> GetStatusList()
        {
            var StatusList = _employeeRepository.GetStatusList();
            return StatusList;
        }

        public TaskStatusCountDm GetTaskCounts(long id)
        {
            var TaskCounts = _employeeRepository.GetTaskCounts(id);
            return TaskCounts;
        }

        public bool UpdateTask(long id, long status)
        {
            var result = _employeeRepository.UpdateTaskStatus(id, status);
            return result;
        }

        public TaskDm GetTaskDetails(long Id)
        {
            var SingleTaskDetails = _employeeRepository.GetTaskDetails(Id);
            return SingleTaskDetails;
        }
    }
}
