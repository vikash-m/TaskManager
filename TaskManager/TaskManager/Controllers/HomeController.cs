using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskServiceLayer;
using System.Web.Mail;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        EmployeeService employeeService = new EmployeeService();
        public ActionResult Index()
        {
            //getData();--zafar
            return View();
        }

        public ActionResult Index1()
        {
            var result = employeeService.GetEmployees();

            employeeService.GetEmployees();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult GetUserDetails()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GettUserDetails()
        {
            return View();
        }
    }
}