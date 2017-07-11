using NLog;
using System;
using System.Collections.Generic;
using System.Web.Http;
using TaskDomain.DomainModel;
using TaskManagerDAL.CustomException;
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
            try
            {

                try
                {
                    return _adminRepository.CreateUser(userDetail);
                }
                catch
                {
                    throw new TaskManagerException("Error at Creating User method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }
        [HttpGet, Route("user-detail")]
        public List<UserDetailDm> GetUserDetail()
        {
            try
            {
                try
                {
                    return _adminRepository.GetUser();
                }
                catch
                {
                    throw new TaskManagerException("Error at Listing UserDetails method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }
        [HttpGet, Route("roles/{roleId}")]
        public string GetRoleById(int roleId)
        {
            try
            {
                try
                {
                    return _adminRepository.GetRoleNameById(roleId);
                }
                catch
                {
                    throw new TaskManagerException("Error at Roles of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }
        [HttpPost, Route("login-user")]
        public bool CreateLoginUser(UserLoginDetails loginDetails)
        {
            //fetch emp id based on udm.EmailId
            var employee = _adminRepository.GetUserDetailByEmailId(loginDetails.EmailId);
            try
            {
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
                catch
                {
                    throw new TaskManagerException("Error at Login By Email method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }
        [HttpGet, Route("roles")]
        public List<RoleDm> GetRoles()
        {
            try {
                try
                {

                    return _adminRepository.GetRoles();
                }
                catch
                {
                    throw new TaskManagerException("Error at Listing Roles method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }
        public string GetUserDetailsAddedBy(string id)
        {
            try {
                try
                {
                    return _adminRepository.GetManagerNameById(id);
                }
                catch
                {
                    throw new TaskManagerException("Error at fetching UserDetail (AddedBy) method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }
        [HttpGet, Route("manager")]
        public List<UserDetailDm> GetManagerByRoleId()
        {
            try
            {

                try
                {
                    return _adminRepository.GetManagerByRoleId();
                }
                catch
                {
                    throw new TaskManagerException("Error at ManagerById method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }
        public string GetManagerNameById(string managerId)
        {
            try {
                try
                {


                    return _adminRepository.GetManagerNameById(managerId);
                }
                catch
                {
                    throw new TaskManagerException("Error at ManagerNameBy Id method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }
        [HttpPut, Route("{id}")]
        public bool UpdateUserDetail(UserDetail userDetail)
        {
            try {
                try
                {
                    return _adminRepository.UpdateUserDetail(userDetail);
                }
                catch
                {
                    throw new TaskManagerException("Error at Updating UserDetails method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }
        [HttpDelete, Route("{id}")]
        public bool DeleteUser(string id, string loginUser)
        {
            try
            {

                try
                {
                    return _adminRepository.DeleteUser(id);
                }
                catch
                {
                    throw new TaskManagerException("Error at deleting User method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }
        [HttpGet, Route("{employeeId}")]
        public UserDetail GetUserDetailById(string employeeId)
        {
            try
            {

                try
                {
                    return _adminRepository.GetUserDetailById(employeeId);
                }
                catch
                {
                    throw new TaskManagerException("Error at UserDetailBy method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpGet, Route("email")]
        public bool CheckForEmail(string emailId)
        {
            try {
                try
                {
                    return _adminRepository.CheckForEmail(emailId);
                }
                catch
                {
                    throw new TaskManagerException("Error at Email method of Admin controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return false;
            }
        }

    }
}
