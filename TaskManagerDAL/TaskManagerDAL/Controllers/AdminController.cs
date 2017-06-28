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
        [HttpGet,Route("roles/{roleId}")]
        public string GetRoleById(int roleId)
        {
            return _adminRepository.GetRoleById(roleId);
        }
        [HttpPost, Route("login-user")]
        public bool CreateLoginUser(UserDetail UserDetail, string password)
        {
            //fetch emp id based on udm.EmailId
            var employee = _adminRepository.GetUserDetailByEmailId(UserDetail.EmailId);

            try
            {
                var loginUserDetails = new LoginUser
                {
                    RoleId = UserDetail.RoleId,
                    EmpId = employee.Id,
                    UserName = UserDetail.EmailId,
                    Password = password,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };
                var result = _adminRepository.CreateLoginUser(loginUserDetails);
                return result;
            }

            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet, Route("roles")]
        public List<Role> GetRoles()
        {
            return _adminRepository.GetRoles();
        }

        [HttpGet, Route("manager")]
        public List<UserDetail> GetManagerByRoleId(int roleId)
        {
            return _adminRepository.GetManagerByRoleId(roleId);
        }

        [HttpPut, Route("{id}")]
        public bool UpdateUserDetail(UserDetail userDetail)
        {
            return _adminRepository.UpdateUserDetail(userDetail);
        }

        [HttpDelete, Route("{id}")]
        public bool DeleteUser(string id,string loginUser)
        {
            return _adminRepository.DeleteUser(id);
        }

        [HttpGet, Route("{employeeId}")]
        public UserDetail GetUserDetailById(string employeeId)
        {
            return _adminRepository.GetUserDetailById(employeeId);
        }
    }
}
