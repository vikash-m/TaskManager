using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain.DomainModel;
using TaskDAL.Repository;

namespace TaskServiceLayer
{
    public class EmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        public List<EmployeeModelDm> GetEmployees()
        {
            var EmployeeList =  employeeRepository.GetEmployee();
            return EmployeeList;
        }

        public List<TaskDm> GetEmployeeTasks(long id)
        {
            var EmployeeTasks = employeeRepository.GetEmployeeTasks(id);
            return EmployeeTasks;
        }

        public List<TaskStatuDm> GetStatusList()
        {
            var StatusList = employeeRepository.GetStatusList();
            return StatusList;
        }

        public TaskStatusCountDm GetTaskCounts(long id)
        {
            var TaskCounts = employeeRepository.GetTaskCounts(id);
            return TaskCounts;
        }

        public bool UpdateTask(long id, long status)
        {
            var result = employeeRepository.updatetask(id, status);
            return result;
        }

        public TaskDm GetTaskDetails(long Id)
        {
            var SingleTaskDetails = employeeRepository.GetTaskDetails(Id);
           return SingleTaskDetails;
        }
    }
}
