using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManagerUtility;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("email")]
    public class EmailController : ApiController
    {
        private static readonly string DalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"];
        [HttpGet, Route("verify")]
        public async Task<string> Verify(string username)
        {
            var status = string.Empty;
            var decrypt = new EncryptionDecryption();
            var emailId = decrypt.Decrypt(username);
            var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
            var response = await client.GetAsync($"/login/verify/?emailId={emailId}");
            if (response.IsSuccessStatusCode)
                status = response.Content.ReadAsAsync<string>().Result;
            return status;

        }
    }
}
