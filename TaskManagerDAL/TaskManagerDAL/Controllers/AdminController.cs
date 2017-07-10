using NLog;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TaskDomain.DomainModel;
using TaskManagerDAL.DAL;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("admin")]
    public class AdminController : ApiController
    {
        private readonly AdminRepository _adminRepository = new AdminRepository();
        Logger logger = LogManager.GetCurrentClassLogger();

        [HttpPost, Route("create-user")]
        public bool CreateUser(UserDetail userDetail)
        {
            return _adminRepository.CreateUser(userDetail);
        }
        [HttpGet, Route("user-detail")]
        public List<UserDetailDm> GetUserDetail()
        {
            return _adminRepository.GetUser();
        }
        [HttpGet, Route("roles/{roleId}")]
        public string GetRoleById(int roleId)
        {
            return _adminRepository.GetRoleNameById(roleId);
        }
        [HttpPost, Route("login-user")]
        public bool CreateLoginUser(UserLoginDetails loginDetails)
        {
            //fetch emp id based on udm.EmailId
            var employee = _adminRepository.GetUserDetailByEmailId(loginDetails.EmailId);
            try
            {
                var loginUserDetails = new LoginUser
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = employee.RoleId,
                    EmpId = employee.Id,
                    UserName = employee.EmailId,
                    Password = loginDetails.Password,
                    CreateDate = DateTime.Now

                };
                var result = _adminRepository.CreateLoginUser(loginUserDetails);
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error Occured");
                return false;
            }
        }
        [HttpGet, Route("roles")]
        public List<RoleDm> GetRoles()
        {
            return _adminRepository.GetRoles();
        }
        public string GetUserDetailsAddedBy(string id)
        {
            return _adminRepository.GetManagerNameById(id);
        }
        [HttpGet, Route("manager")]
        public List<UserDetailDm> GetManagerByRoleId()
        {
            return _adminRepository.GetManagerByRoleId();
        }
        public string GetManagerNameById(string managerId)
        {
            return _adminRepository.GetManagerNameById(managerId);
        }
        [HttpPut, Route("{id}")]
        public bool UpdateUserDetail(UserDetail userDetail)
        {
            return _adminRepository.UpdateUserDetail(userDetail);
        }
        [HttpDelete, Route("{id}")]
        public bool DeleteUser(string id, string loginUser)
        {
            return _adminRepository.DeleteUser(id);
        }
        [HttpGet, Route("{employeeId}")]
        public UserDetail GetUserDetailById(string employeeId)
        {
            return _adminRepository.GetUserDetailById(employeeId);
        }

        [HttpGet, Route("email")]
        public bool CheckForEmail(string emailId)
        {
            return _adminRepository.CheckForEmail(emailId);
        }

    }
}
