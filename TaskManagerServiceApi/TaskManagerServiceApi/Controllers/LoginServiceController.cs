using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NLog;
using TaskDomain.DomainModel;
using TaskManagerServiceApi.Content.Resources;
using TaskManagerUtility;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("login")]
    public class LoginServiceController : ApiController
    {
        private static readonly string DalLayerUrl = DALLayerLinkResources.DalLayerUrl;
        Logger logger = LogManager.GetCurrentClassLogger(); 

        [HttpGet, Route("login-details")]
        public async Task<LoginUserDm> GetLoginDetails(string name, string password)
        {
            var encryptionDecryption = new EncryptionDecryption();
            //Password will encrypted.
            var encryptedPassword = encryptionDecryption.Encrypt(password);

            var loginUser = new LoginUserDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getLoginDetailsUrl , name) + HttpContext.Current.Server.UrlEncode(encryptedPassword));


                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    loginUser = response.Content.ReadAsAsync<LoginUserDm>().Result;


            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return loginUser;
        }
        [HttpGet, Route("{id}")]
        public async Task<UserDetailDm> GetUserDetailsData(string id)
        {
            var userDetail = new UserDetailDm();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync(string.Format(DALLayerLinkResources.getUserDetailsDataUrl , id));

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    userDetail = response.Content.ReadAsAsync<UserDetailDm>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
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
            var response = await client.PostAsJsonAsync(DALLayerLinkResources.addLoginUserDetailsUrl, loginDetails);
            var status = new bool();
            if (response.IsSuccessStatusCode)
            {
                status = response.Content.ReadAsAsync<bool>().Result;
            }
            return status;
        }

    }
}