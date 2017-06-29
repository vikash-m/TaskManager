using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskDomain.DomainModel;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("admin")]
    public class AdminServiceController : ApiController
    {
        private static readonly string DalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"];

        [HttpPost, Route("create-user")]
        public async Task<bool> CreateUsers(string loginUser, UserDetailDm userDetail)
        {
            var createStatus = new bool();
            try
            {
                userDetail.Id = Guid.NewGuid().ToString();
                userDetail.CreatedBy = loginUser;
                userDetail.CreateDate = DateTime.Now;

                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PostAsJsonAsync("/admin/create-user", userDetail);

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    createStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return createStatus;
        }

        [HttpGet, Route("roles")]
        public async Task<List<RoleDm>> GetRoles()
        {
            var roles = new List<RoleDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync("/admin/roles");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    roles = response.Content.ReadAsAsync<List<RoleDm>>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return roles;
        }

        [HttpGet, Route("manager")]
        public async Task<List<UserDetailDm>> GetManagerByRoleId()
        {
            var managers = new List<UserDetailDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/admin/manager");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    managers = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return managers;
        }

        [HttpGet, Route("user-detail")]
        public async Task<List<UserDetailDm>> GetUserDetail()
        {
            var users = new List<UserDetailDm>();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/admin/user-detail/");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    users = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;
                foreach (var item in users)
                {
                    if (item != null)
                    {
                        var id = item.Id;
                        var roleResponse = await client.GetAsync($"/admin/roles/{item.RoleId}");
                        if (roleResponse.IsSuccessStatusCode)
                        {
                            item.RoleName = roleResponse.Content.ReadAsAsync<string>().Result;
                        }
                    }
                    if (item.ManagerName == null)
                    {
                        item.ManagerName = "No Manager";
                    }
                }
                // }
                //foreach(var item in users)
                //{
                //    if(item!=null)
                ////    {
                //        var mgrResponse = await client.GetAsync($"/admin/manager/{item.ManagerId}");
                //        if(mgrResponse.IsSuccessStatusCode)
                //        {
                //            item.ManagerName = mgrResponse.Content.ReadAsAsync<string>().Result;
                //        }
                //   }
                // }
            }
            catch (Exception ex)
            {
                throw;
            }
            return users;
        }

        [HttpGet, Route("search")]
        public async Task<List<UserDetailDm>> Search(string SearchText)
        {
            var user = new List<UserDetailDm>();
            try
            {
                string URL = DalLayerUrl + "/admin/Search";
                HttpClient client = new HttpClient();
                string urlParameters = "?SearchText=" + SearchText;
                client.BaseAddress = new Uri(URL);

                var response = await client.GetAsync(urlParameters);

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    user = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
            return user;
        }

        [HttpGet, Route("{employeeId}")]
        public async Task<UserDetailDm> EditUser(string employeeId)
        {
            var user = new UserDetailDm();
            try
            {

                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/admin/{employeeId}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    user = response.Content.ReadAsAsync<UserDetailDm>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        [HttpPut, Route("{id}")]
        public async Task<bool> EditUserDetails(string id, string loginUser, UserDetailDm userDetail)
        {
            var updateStatus = new bool();
            try
            {
                //userDetail.Id = Guid.NewGuid().ToString();
                userDetail.ModifiedBy = loginUser;
                userDetail.ModifiedDate = DateTime.Now;
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PutAsJsonAsync($"/admin/{userDetail.Id}", userDetail);

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    updateStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return updateStatus;
        }

        [HttpDelete, Route("{id}")]
        public async Task<bool> DeleteUser(string id, string loginUser)
        {
            var user = new UserDetailDm();

            var updateStatus = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.DeleteAsync($"/admin/{id}/?loginUser={loginUser}");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    updateStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return updateStatus;
        }
    }
}

