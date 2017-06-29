using System.Web.Http;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using System;
using TaskDomain.DomainModel;
using TaskManagerUtility;
namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("login")]
    public class LoginServiceController : ApiController
    {
        private static readonly string DalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"];

        [HttpGet, Route("login-details")]
        public async Task<LoginUserDm> GetLoginDetails(string name, string password)
        {
            //var password=encr
            var encryptionDecryption = new EncryptionDecryption();
            var encryptedPassword = encryptionDecryption.Encrypt(password);

            var loginUser = new LoginUserDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/login/?name={name}&password={encryptedPassword}");

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
                var response = await client.GetAsync($"/login/user/{id}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    userDetail = response.Content.ReadAsAsync<UserDetailDm>().Result;

                var roleName = await client.GetAsync($"/login/roles/{userDetail.RoleId}");
                if (roleName.IsSuccessStatusCode)
                    userDetail.RoleName = roleName.Content.ReadAsAsync<string>().Result;
                //userDetail.RoleName = roleName.Content.ReadAsAsync<String>().Result;
            }
            catch
            {
                throw;
            }

            return userDetail;

        }

        [HttpPost, Route("")]
        public async Task<bool> AddLoginUserDetails(UserLoginDetails loginDetails)
        {
            var encryptDecryt = new EncryptionDecryption();
            var encryptedPassword = encryptDecryt.Encrypt(loginDetails.Password);
            loginDetails.Password = encryptedPassword;
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var response = await client.PostAsJsonAsync("admin/login-user", loginDetails);
            var status = new bool();
            if (response.IsSuccessStatusCode)
            {
                status = response.Content.ReadAsAsync<bool>().Result;
            }
            return status;
        }

    }
}