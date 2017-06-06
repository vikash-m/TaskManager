using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDAL.Repository;
using TaskDomain.DomainModel;

namespace TaskServiceLayer
{
   public class LoginServices
    {
        LoginRepository loginRepository = new LoginRepository();
        public LoginUserDm getLogDetails(string Name ,string Password)
        {
            var logDetails = loginRepository.getLogDetails(Name, Password);
            return logDetails;
        }
    }
}
