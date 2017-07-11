using NLog;
using System.Web.Http;
using TaskDomain.DomainModel;
using TaskManagerDAL.CustomException;
using TaskManagerDAL.DAL;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private readonly LoginRepository _loginRepository = new LoginRepository();
        private readonly RolesRepository _rolesRepository = new RolesRepository();
        Logger logger = LogManager.GetCurrentClassLogger();
        [HttpGet, Route("")]
        public LoginUserDm GetLoginUserDetail(string name, string password)
        {
            try {
                try
                {
                    return _loginRepository.GetLoginUserDetails(name, password);
                }
                catch
                {
                    throw new TaskManagerException("Error at Login UserDetail method of Login controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpGet, Route("user/{id}")]
        public UserDetailDm GetUserDetailsData(string id)
        {
            try {
                try
                {
                    return _loginRepository.GetUserDetailsData(id);
                }
                catch
                {
                    throw new TaskManagerException("Error at Login UserDetailData method of Login controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpGet, Route("roles/{id}")]
        public string GetRoleByRoleId(int id)
        {
            try {
                try
                {
                    return _rolesRepository.GetRoleNameById(id);
                }
                catch
                {
                    throw new TaskManagerException("Error at Role by RoleId method of Login controller in DAL layer.");
                }
            }
            catch (TaskManagerException taskManagerException)
            {
                logger.Error(taskManagerException, taskManagerException.Message);
                return null;
            }
        }

        [HttpGet, Route("verify")]
        public string GetEmailIfExist(string emailId) => _loginRepository.GetEmailIfExist(emailId);
    }

}
