using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using TaskDomain.DomainModel;

namespace TaskManagerServiceApi.Controllers
{
    public class LoginSreviceController : ApiController
    {
        static HttpClient client = new HttpClient();
        private static string dalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"] + "/Login";
        private string urlParameters;

        public async Task<LoginUserDm> GetLogDetails(string name, string password)
        {
            try
            {
                string URL = dalLayerUrl + "/GetLogDetails";
                HttpClient client = new HttpClient();
                urlParameters = "?name=" + name + "&password=" + password;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<LoginUserDm>().Result;
                    return dataObjects;
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserdetailDm> GetUserDetailsData(int id)
        {
            try
            {
                string URL = dalLayerUrl + "/GetUserDetailsData";
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

    }
}