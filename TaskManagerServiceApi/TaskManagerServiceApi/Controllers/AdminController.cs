using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TaskDomain.DomainModel;
using TaskManagerUtility;

namespace TaskManagerServiceApi.Controllers
{
    [RoutePrefix("admin")]
    public class AdminController : ApiController
    {
        private static readonly string DalLayerUrl = ConfigurationManager.AppSettings["dalLayerUrl"];
        Logger logger = LogManager.GetCurrentClassLogger();
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
                if (createStatus)
                {
                    var emailUtility = new EmailUtility();
                    emailUtility.SendMail(userDetail);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
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
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return users;
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return null;
            }
            return user;
        }
        [HttpPut, Route("{id}")]
        public async Task<bool> EditUserDetails(string id, string loginUser, UserDetailDm userDetail)
        {
            var updateStatus = new bool();
            try
            {
                userDetail.ModifiedBy = loginUser;
                userDetail.ModifiedDate = DateTime.Now;
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.PutAsJsonAsync($"/admin/{userDetail.Id}", userDetail);
                if (response.IsSuccessStatusCode)
                    // Parse the response body. Blocking!
                    updateStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
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
                // Parse the response body. Blocking!
                updateStatus = response.Content.ReadAsAsync<bool>().Result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
            }
            return updateStatus;
        }
        [HttpGet, Route("email")]
        public async Task<bool> CheckForEmail(string emailId)
        {
            var emailExist = new bool();
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(DalLayerUrl) };
                var response = await client.GetAsync($"/admin/email/?emailId={emailId}");
                if (response.IsSuccessStatusCode)
                {
                    emailExist = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
            }
            return emailExist;
        }


    }
}

