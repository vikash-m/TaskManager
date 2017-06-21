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

        [HttpPost, Route("")]
        public async Task<bool> CreateUsers(UserDetailDm userdetail)
        {
            var createStatus = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PostAsJsonAsync("/admin", userdetail);

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
                var response = await client.GetAsync($"/admin/manager?roleId={EnumClass.Roles.Manager}");

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
                var response = await client.GetAsync($"/admin/user-detail");

                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    users = response.Content.ReadAsAsync<List<UserDetailDm>>().Result;
            }
            catch (Exception)
            {
                throw;
            }
            return users;
        }

        [HttpGet, Route("{employeeId}")]
        public async Task<UserDetailDm> EditUser(int employeeId)
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
        public async Task<bool> UpdateUserDetails(UserDetailDm userdetailDm)
        {
            var updateStatus = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PutAsJsonAsync($"/admin/{userdetailDm.Id}", userdetailDm);

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
        public async Task<bool> DeleteUser(int id)
        {
            var user = new UserDetailDm();
            var updateStatus = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PutAsJsonAsync($"/admin/{id}", user);

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