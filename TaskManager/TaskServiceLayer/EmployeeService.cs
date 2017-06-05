using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain;
using TaskDAL.Repository;

namespace TaskServiceLayer
{
    public class EmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        public List<EmployeeModel> GetEmployees()
        {
            var EmployeeList =  employeeRepository.GetEmployee();
            return EmployeeList;
        }
    }
}
