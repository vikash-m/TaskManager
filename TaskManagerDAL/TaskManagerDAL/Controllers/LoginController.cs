﻿using System.Threading.Tasks;
using System.Web.Http;
using TaskManagerDAL.DAL;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private readonly LoginRepository _loginRepository = new LoginRepository();

        [HttpGet, Route("")]
        public LoginUser GetLoginUserDetail(string name, string password)
        {
            return _loginRepository.GetLoginUserDetails(name, password);
        }

        [HttpGet, Route("{id}")]
        public UserDetail GetUserDetailsData(int id)
        {
            return _loginRepository.GetUserDetailsData(id);
        }
    }
}