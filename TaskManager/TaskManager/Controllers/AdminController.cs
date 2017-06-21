using System.IO;
using System.Web.Mvc;
using TaskDomain.DomainModel;
using PagedList;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;


namespace TaskManager.Controllers
{
    public class AdminController : Controller
    {
        private static string serviceLayerUrl = ConfigurationManager.AppSettings["serviceLayerUrl"] + "/AdminService";
        private string urlParameters;

        [HttpGet]
        public async Task<ActionResult> SaveUserDetails()
        {

            try
            {

                var url = serviceLayerUrl + "/DropdownRoles";
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);

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

                var uRlDropdownManager = serviceLayerUrl + "/DropdownMgr";
                client.BaseAddress = new Uri(uRlDropdownManager);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage responseDropdownManager = await client.GetAsync(uRlDropdownManager);
                if (responseDropdownManager.IsSuccessStatusCode)
                {
                    var mgrResult = responseDropdownManager.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
                    ViewBag.List1 = mgrRes;
                }
                return View();
            }
            catch
            {
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<ActionResult> SaveUserDetails(UserDetailDm ud)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (!ModelState.IsValid) return RedirectToAction("SaveUserDetails");

                var url = serviceLayerUrl + "/SaveUsers";
                var client = new HttpClient();
                urlParameters = "?ud=" + ud;
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<bool>().Result;
                    return RedirectToAction(result ? "ViewUserDetails" : "SaveUserDetails");
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }
        public async Task<ActionResult> ViewUserDetails(int? page)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                if (null == user) return RedirectToAction("Login", "Login");

                var id = user.Id;
                var url = serviceLayerUrl + "/ViewUser";
                var client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<List<UserDetailDm>>().Result.ToPagedList(page ?? 1, 10); ;
                    return View(dataObjects);
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<ActionResult> EditUserDetails(int id)
        {
            try
            {

                var URL = serviceLayerUrl + "/DropdownRoles";
                var client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    var roleResult = response.Content.ReadAsAsync<List<RoleDm>>().Result;
                    var roles = new SelectList(roleResult, "RoleId", "RoleName");
                    ViewBag.List = roles;
                }

                var uRlDropdownManager = serviceLayerUrl + "/DropdownMgr";
                client.BaseAddress = new Uri(uRlDropdownManager);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var responseDropdownManager = await client.GetAsync(uRlDropdownManager);
                if (responseDropdownManager.IsSuccessStatusCode)
                {
                    var mgrResult = responseDropdownManager.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
                    ViewBag.List1 = mgrRes;
                }

                var uRlEditUser = serviceLayerUrl + "/EditUser";
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(uRlEditUser);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var responseEditUser = await client.GetAsync(urlParameters);
                if (responseEditUser.IsSuccessStatusCode)
                {
                    var res = responseEditUser.Content.ReadAsAsync<UserDetailDm>().Result;
                    return View(res);
                }
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditUserDetails(UserDetailDm udm)
        {
            try
            {

                var url = serviceLayerUrl + "/SaveEditUser";
                var client = new HttpClient();
                urlParameters = "?udm=" + udm;
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<bool>().Result;
                    return RedirectToAction("ViewUserDetails");
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {

                var url = serviceLayerUrl + "/DeleteUser";
                var client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<bool>().Result;
                    return RedirectToAction("ViewUserDetails");
                }
                return null;
            }
            catch
            {
                return View("Error");
            }
        }
    }
}