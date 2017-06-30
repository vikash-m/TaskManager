using System.Web.Http;
using TaskManagerDAL.DAL;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private readonly LoginRepository _loginRepository = new LoginRepository();
        private readonly RolesRepository _rolesRepository = new RolesRepository();

        [HttpGet, Route("")]
        public LoginUser GetLoginUserDetail(string name, string password)
        {
            return _loginRepository.GetLoginUserDetails(name, password);
        }

        [HttpGet, Route("user/{id}")]
        public UserDetail GetUserDetailsData(string id)
        {
            return _loginRepository.GetUserDetailsData(id);
        }

        [HttpGet, Route("roles/{id}")]
        public string GetRoleByRoleId(int id)
        {
            return _rolesRepository.GetRoleNameById(id);
        }

        [HttpGet, Route("verify")]
        public string GetEmailIfExist(string emailId) => _loginRepository.GetEmailIfExist(emailId);
    }

}
