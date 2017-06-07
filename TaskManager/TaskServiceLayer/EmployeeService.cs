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

        public List<TaskDm> GetEmployeeTasks()
        {
            var EmployeeTasks = employeeRepository.GetEmployeeTasks();
            return EmployeeTasks;
        }

        public TaskStatusCountDm GetTaskCounts()
        {
            var TaskCounts = employeeRepository.GetTaskCounts();
            return TaskCounts;
        }
    }
}
