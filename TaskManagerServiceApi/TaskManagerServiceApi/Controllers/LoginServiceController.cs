using System.Web.Http;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using System;
using TaskDomain.DomainModel;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("login")]
    public class LoginServiceController : ApiController
    {
        private static readonly string DalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"];

        [HttpGet, Route("login-details")]
        public async Task<LoginUserDm> GetLoginDetails(string name, string password)
        {
            var loginUser = new LoginUserDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/login/?name={name}&password={password}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    loginUser = response.Content.ReadAsAsync<LoginUserDm>().Result;


            }
            catch (Exception)
            {
                throw;
            }
            return loginUser;
        }
        [HttpGet, Route("{id}")]
        public async Task<UserDetailDm> GetUserDetailsData(int id)
        {
            var userDetail = new UserDetailDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/login/{id}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    userDetail = response.Content.ReadAsAsync<UserDetailDm>().Result;


            }
            catch (Exception e)
            {
                throw;
            }

            return userDetail;

        }

    }
}