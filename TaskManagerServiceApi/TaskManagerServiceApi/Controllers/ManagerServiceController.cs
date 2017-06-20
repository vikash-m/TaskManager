using System.Web.Mvc;
using System.Web.Http;
using System.Configuration;
using System.Net.Http;
using TaskDomain.DomainModel;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TaskManagerServiceApi.Controllers
{
    public class ManagerServiceController : ApiController
    {
        static HttpClient client = new HttpClient();
        private static string dalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"] + "/Manager";
        private string urlParameters;

        public async Task<List<UserdetailDm>> GetEmployeesDetailsByManagerId(long ManagerId)
        {
            try
            {
                string URL = dalLayerUrl + "/GetEmployeesDetailsByManagerId";
                HttpClient client = new HttpClient();
                urlParameters = "?ManagerId=" + ManagerId;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
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

        public async Task<TaskDm> AddTask(TaskDm task, long loginUserId)
        {
            try
            {
                task.CreateDate = DateTime.Now;
                task.CreatedBy = loginUserId;

                string URL = dalLayerUrl + "/AddTask";
                HttpClient client = new HttpClient();
                urlParameters = "?task=" + task;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, task);
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

        public async Task<bool> AddTaskDocument(TaskDocumentDm taskDocument, long loginUserId)
        {
            try
            {
                taskDocument.CreateDate = DateTime.Now;
                taskDocument.AddedBy = loginUserId;

                string URL = dalLayerUrl + "/AddTaskDocument";
                HttpClient client = new HttpClient();
                urlParameters = "?taskDocument=" + taskDocument;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, taskDocument);
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

        public async Task<List<TaskDm>> GetAllTask(long managerId)
        {
            try
            {
                string URL = dalLayerUrl + "/GetAllTask";
                HttpClient client = new HttpClient();
                urlParameters = "?managerId=" + managerId;
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

        public async Task<bool> UpdateTask(TaskDm task)
        {
            try
            {
                task.ModifiedDate = DateTime.Now;

                string URL = dalLayerUrl + "/UpdateTask";
                HttpClient client = new HttpClient();
                urlParameters = "?task=" + task;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, task);
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

        public async Task<bool> DeleteTask(long? id)
        {
            try
            {
               
                string URL = dalLayerUrl + "/DeleteTask";
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

        public async Task<string> GetTaskNameByTaskId(long? id)
        {
            try
            {
                string URL = dalLayerUrl + "/GetTaskNameByTaskId";
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
                    var dataObjects = response.Content.ReadAsAsync<string>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteTaskDocument(TaskDocumentDm taskDocument)
        {
            try
            {
                taskDocument.ModifiedDate = DateTime.Now;

                string URL = dalLayerUrl + "/DeleteTaskDocument";
                HttpClient client = new HttpClient();
                urlParameters = "?taskDocument=" + taskDocument;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, taskDocument);
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

        public async Task<TaskDm> GetTaskByTaskId(long? id)
        {
            try
            {
                string URL = dalLayerUrl + "/GetTaskByTaskId";
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

        public async Task<bool> GetTaskNames(string title)
        {
            try
            {
                string URL = dalLayerUrl + "/GetTaskNames";
                HttpClient client = new HttpClient();
                urlParameters = "?title=" + title;
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