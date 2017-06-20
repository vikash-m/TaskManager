using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using TaskDomain.DomainModel;

namespace TaskManagerServiceApi.Controllers
{
    public class UserServiceController : ApiController
    {
        static HttpClient client = new HttpClient();
        private static string dalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"] + "/User";
        private string urlParameters;

        public async Task<bool>  SaveUsers(UserdetailDm userdetail)
        {
            try
            {
                
                string URL = dalLayerUrl + "/SaveUser";
                HttpClient client = new HttpClient();
                urlParameters = "?userdetail=" + userdetail;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, userdetail);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<RoleModel>> DropdownRoles()
        {
            try
            {
                string URL = dalLayerUrl + "/DropdownRoles";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<RoleModel>>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<UserdetailDm>> DropdownMgr()
        {
            try
            {
                string URL = dalLayerUrl + "/DropdownMgr";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<UserdetailDm>>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<UserdetailDm>> ViewUser()
        {
            try
            {
                string URL = dalLayerUrl + "/ViewUser";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<UserdetailDm>>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<UserdetailDm> EditUser(int id)
        {
            try
            {
                string URL = dalLayerUrl + "/EditUser";
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
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<UserdetailDm>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> SaveEditUser(UserdetailDm udm)
        {
            try
            {

                string URL = dalLayerUrl + "/SaveEditUser";
                HttpClient client = new HttpClient();
                urlParameters = "?udm=" + udm;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, udm);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {

                string URL = dalLayerUrl + "/DeleteUser";
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
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}