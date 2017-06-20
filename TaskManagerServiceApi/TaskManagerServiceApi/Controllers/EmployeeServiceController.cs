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
    public class EmployeeServiceController : ApiController
    {
        // GET: EmployeeService
        static HttpClient client = new HttpClient();
        private static string dalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"] + "/Employee";
        private string urlParameters;

        public async Task<List<EmployeeModelDm>> GetEmployees()
        {
            try
            {
                string URL = dalLayerUrl + "/GetEmployee";
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
                    var dataObjects = response.Content.ReadAsAsync<List<EmployeeModelDm>>().Result;
                    return dataObjects;
                }
                
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TaskDm>> GetEmployeeTasks(long id)
        {
            try
            {
                string URL = dalLayerUrl + "/GetEmployeeTasks";
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
                    var dataObjects = response.Content.ReadAsAsync<List<TaskDm>>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TaskStatuDm>> GetStatusList()
        {
            try
            {
                string URL = dalLayerUrl + "/GetStatusList";
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
                    var dataObjects = response.Content.ReadAsAsync<List<TaskStatuDm>>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TaskStatusCountDm> GetTaskCounts(long id)
        {
            try
            {
                string URL = dalLayerUrl + "/GetTaskCounts";
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
                    var dataObjects = response.Content.ReadAsAsync<TaskStatusCountDm>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateTask(long id, long status)
        {
            try
            {
                string URL = dalLayerUrl + "/UpdateTaskStatus";
                HttpClient client = new HttpClient();
                urlParameters = "?id=" + id + "&status=" + status;
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

        public async  Task<TaskDm> GetTaskDetails(long Id)
        {
            try
            {
                string URL = dalLayerUrl + "/GetTaskDetails";
                HttpClient client = new HttpClient();
                urlParameters = "?Id=" + Id;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<TaskDm>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    
}
}