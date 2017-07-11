using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;
using TaskDomain.DomainModel;
using TaskManager.Content.Resources;

namespace TaskManager.Controllers
{
    public class AdminController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string ServiceLayerUrl = ServiceLayerLinkResource.serviceLayerUrl + ServiceLayerLinkResource.AdminUrl;

        [HttpGet]
        public async Task<ActionResult> SaveUserDetails()
        {
            try
            {
                var url = ServiceLayerUrl + ServiceLayerLinkResource.roleUrl;
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var roleResult = response.Content.ReadAsAsync<List<RoleDm>>().Result;
                    var roles = new SelectList(roleResult, "RoleId", "RoleName");
                    ViewBag.List = roles;
                }
                var uRlDropdownManager = ServiceLayerUrl + ServiceLayerLinkResource.managerUrl;
                // List data response.
                var responseDropdownManager = await client.GetAsync(uRlDropdownManager);
                if (responseDropdownManager.IsSuccessStatusCode)
                {
                    var mgrResult = responseDropdownManager.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
                    ViewBag.List1 = mgrRes;
                }
                return View();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> SaveUserDetails(UserDetailDm userDetailDm)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                var name = user.Id;
                if (!ModelState.IsValid) return RedirectToAction("SaveUserDetails");
                var url = ServiceLayerUrl + ServiceLayerLinkResource.createUserUrl;
                var client = new HttpClient { BaseAddress = new Uri(url) };
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                var response = await client.PostAsJsonAsync($"{url}?loginUser={name}", userDetailDm);
                var result = new bool();
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<bool>().Result;

                }
                return RedirectToAction(result ? "ViewUserDetails" : "SaveUserDetails");
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }
        public async Task<ActionResult> ViewUserDetails(/*int? page*/)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");
                var url = ServiceLayerUrl + ServiceLayerLinkResource.userDetailUrl;
                var client = new HttpClient { BaseAddress = new Uri(url) };
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode) return View();
                var dataObjects = response.Content.ReadAsAsync<List<UserDetailDm>>().Result/*.ToPagedList(page ?? 1, 10)*/;
                return View(dataObjects);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> EditUserDetails(string id)
        {
            try
            {
                var url = ServiceLayerUrl + ServiceLayerLinkResource.roleUrl;
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var roleResult = response.Content.ReadAsAsync<List<RoleDm>>().Result;
                    var roles = new SelectList(roleResult, "RoleId", "RoleName");
                    ViewBag.List = roles;
                }
                var uRlDropdownManager = ServiceLayerUrl + ServiceLayerLinkResource.managerUrl;
                // List data response.
                var responseDropdownManager = await client.GetAsync(uRlDropdownManager);
                if (responseDropdownManager.IsSuccessStatusCode)
                {
                    var mgrResult = responseDropdownManager.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
                    ViewBag.List1 = mgrRes;
                }
                var uRlEditUser = ServiceLayerUrl + $"/{id}";
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                var responseEditUser = await client.GetAsync(uRlEditUser);
                if (!responseEditUser.IsSuccessStatusCode) return View();
                var res = responseEditUser.Content.ReadAsAsync<UserDetailDm>().Result;
                return View(res);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditUserDetails(UserDetailDm userDetailDm)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                var response = await client.PutAsJsonAsync(string.Format(ServiceLayerLinkResource.editUserDetailsUrl, userDetailDm.Id , user.Id), userDetailDm);
                if (!response.IsSuccessStatusCode) return View();
                return RedirectToAction("ViewUserDetails");
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                var response = await client.DeleteAsync(string.Format(ServiceLayerLinkResource.deleteUserUrl, id , user.Id));
                if (!response.IsSuccessStatusCode) return View();
                return RedirectToAction("ViewUserDetails");
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return View("Error");
            }
        }

        public async Task<ActionResult> CheckForEmail(string emailId)
        {
            var client = new HttpClient { BaseAddress = new Uri(ServiceLayerUrl) };
            var result = new bool();

            // List data response.
            var response = await client.GetAsync(string.Format(ServiceLayerLinkResource.checkForMailUrl, emailId));
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<bool>().Result;
            }
            return result == false ? Json("Sorry, this email already exists", JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);

        }
    }
}

