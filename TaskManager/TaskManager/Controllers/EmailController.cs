using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;
using TaskDomain.DomainModel;
using TaskManager.Content.Resources;

namespace TaskManager.Controllers
{
    public class EmailController : Controller
    {
        private static readonly string ServiceLayerUrl = ServiceLayerLinkResource.serviceLayerUrl;
        Logger logger = LogManager.GetCurrentClassLogger();
        // GET: Email
        public async Task<ActionResult> Verify(string username)
        {
            var emailId = string.Empty;
            var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
            var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.verifyUrl , username));
            if (response.IsSuccessStatusCode)
                emailId = response.Content.ReadAsAsync<string>().Result;
            var loginUser = new UserLoginDetails
            {
                EmailId = emailId
            };
            return !string.IsNullOrWhiteSpace(emailId) ? View(loginUser) : View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> Verify(UserLoginDetails loginUser)
        {
            var status = new bool();
            if (ModelState.IsValid)
            {

                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                var response = await client.PostAsJsonAsync(ServiceLayerLinkResource.verifyLoginUrl, loginUser);
                if (response.IsSuccessStatusCode)
                {
                    status = response.Content.ReadAsAsync<bool>().Result;
                }
                return status ? RedirectToAction("Login", "Login") : RedirectToAction("Verify");
            }
            return View();
        }
    }
}