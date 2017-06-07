﻿using System;
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

        public ActionResult Dashboard()
        {
            var taskStatusCounts = employeeService.GetTaskCounts();
            return View(taskStatusCounts);
        }

        public ActionResult MyTasks()
        {
            var employeeTasks = employeeService.GetEmployeeTasks();
            return View(employeeTasks);
        }

    }
}