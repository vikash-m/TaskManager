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

                string URL = serviceLayerUrl + "/DropdownRoles";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    var roleResult = response.Content.ReadAsAsync<List<RoleDm>>().Result;
                    var roles = new SelectList(roleResult, "RoleId", "RoleName");
                    ViewBag.List = roles;
                }

                string uRLDropdownManager = serviceLayerUrl + "/DropdownMgr";
                client.BaseAddress = new Uri(uRLDropdownManager);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage responseDropdownManager = await client.GetAsync(uRLDropdownManager);
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

                string URL = serviceLayerUrl + "/SaveUsers";
                HttpClient client = new HttpClient();
                urlParameters = "?ud=" + ud;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
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
                string URL = serviceLayerUrl + "/ViewUser";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
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

                string URL = serviceLayerUrl + "/DropdownRoles";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    var roleResult = response.Content.ReadAsAsync<List<RoleDm>>().Result;
                    var roles = new SelectList(roleResult, "RoleId", "RoleName");
                    ViewBag.List = roles;
                }

                string uRLDropdownManager = serviceLayerUrl + "/DropdownMgr";
                client.BaseAddress = new Uri(uRLDropdownManager);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage responseDropdownManager = await client.GetAsync(uRLDropdownManager);
                if (responseDropdownManager.IsSuccessStatusCode)
                {
                    var mgrResult = responseDropdownManager.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
                    ViewBag.List1 = mgrRes;
                }

                string uRLEditUser = serviceLayerUrl + "/EditUser";
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(uRLEditUser);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage responseEditUser = await client.GetAsync(urlParameters);
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

                string URL = serviceLayerUrl + "/SaveEditUser";
                HttpClient client = new HttpClient();
                urlParameters = "?udm=" + udm;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
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

                string URL = serviceLayerUrl + "/DeleteUser";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
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