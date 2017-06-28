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
        private static string serviceLayerUrl = ConfigurationManager.AppSettings["serviceLayerUrl"] + "/admin";
        private string urlParameters;

        [HttpGet]
        public async Task<ActionResult> SaveUserDetails()
        {

            try
            {

                var url = serviceLayerUrl + "/roles";

                var client = new HttpClient { BaseAddress = new Uri(serviceLayerUrl) };



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

                var uRlDropdownManager = serviceLayerUrl + "/manager";

                //client.BaseAddress = new Uri(uRlDropdownManager);

                // Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

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
            catch (Exception ex)
            {
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

                var url = serviceLayerUrl + "/create-user";
                var client = new HttpClient();
                // urlParameters = "?userDetailDm=" + userDetailDm;
                client.BaseAddress = new Uri(url);

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
                var url = serviceLayerUrl + "/user-detail";
                var client = new HttpClient();
                //urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<List<UserDetailDm>>().Result.ToPagedList(page ?? 1, 10);

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
        public async Task<ActionResult> EditUserDetails(string id)
        {
            try
            {

                var URL = serviceLayerUrl + "/roles";
                var client = new HttpClient { BaseAddress = new Uri(serviceLayerUrl) };
                //var client = new HttpClient();
                //client.BaseAddress = new Uri(URL);

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

                var uRlDropdownManager = serviceLayerUrl + "/manager";
                //client.BaseAddress = new Uri(uRlDropdownManager);

                //// Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var responseDropdownManager = await client.GetAsync(uRlDropdownManager);
                if (responseDropdownManager.IsSuccessStatusCode)
                {
                    var mgrResult = responseDropdownManager.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                    var mgrRes = new SelectList(mgrResult, "Id", "FirstName");
                    ViewBag.List1 = mgrRes;
                }

                var uRlEditUser = serviceLayerUrl + $"/{id}";
                //urlParameters = "?id=" + id;
                // client.BaseAddress = new Uri(uRlEditUser);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var responseEditUser = await client.GetAsync(uRlEditUser);
                if (responseEditUser.IsSuccessStatusCode)
                {
                    var res = responseEditUser.Content.ReadAsAsync<UserDetailDm>().Result;
                    return View(res);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditUserDetails(UserDetailDm userDetailDm)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                //var userId = user.Id;
                //var id = userDetailDm.Id;
                //  var url = serviceLayerUrl + "/EditUserDetails";
                var client = new HttpClient()
                {
                    //  urlParameters = "?id="+Id;
                    BaseAddress = new Uri(serviceLayerUrl)
                };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.PutAsJsonAsync($"admin/{userDetailDm.Id}/?loginUser={user.Id}", userDetailDm);
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
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                var user = (UserDetailDm)Session["SessionData"];
                // var url = serviceLayerUrl + "/DeleteUser";
                var client = new HttpClient();
               // urlParameters = "?id=" + id;
                client.BaseAddress = new Uri(serviceLayerUrl);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var response = await client.DeleteAsync($"admin/{id}/?loginUser={user.Id}");
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