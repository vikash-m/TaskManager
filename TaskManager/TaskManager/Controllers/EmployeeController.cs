using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using TaskServiceLayer;

namespace TaskManager.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeService employeeService = new EmployeeService();
        public ActionResult Index()
        {
            var employeeTasks = employeeService.GetEmployeeTasks();
            return View(employeeTasks);
        }
    }
}