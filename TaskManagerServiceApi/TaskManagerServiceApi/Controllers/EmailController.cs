using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManagerServiceApi.Content.Resources;
using TaskManagerUtility;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("email")]
    public class EmailController : ApiController
    {
        private static readonly string DalLayerUrl = DALLayerLinkResources.DalLayerUrl;
        [HttpGet, Route("verify")]
        public async Task<string> Verify(string username)
        {
            var status = string.Empty;
            var decrypt = new EncryptionDecryption();
            var emailId = decrypt.Decrypt(username);
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var response = await client.GetAsync(string.Format(DALLayerLinkResources.verifyUrl, emailId));
            if (response.IsSuccessStatusCode)
                status = response.Content.ReadAsAsync<string>().Result;
            return status;
        }
    }
}
