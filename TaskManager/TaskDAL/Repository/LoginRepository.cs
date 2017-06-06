using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain.DomainModel;

namespace TaskDAL.Repository
{
    public class LoginRepository
    {
        TaskManagerEntities taskManagerEntities = new TaskManagerEntities();

      
        public LoginUserDm getLogDetails(string Name ,string Password)
        {
            LoginUserDm log = new LoginUserDm();
            var result = taskManagerEntities.LoginUsers.FirstOrDefault(m => m.UserName == Name && m.Password == Password);
            //  var res= result.Role.RoleName;
            if (result!=null)
            {
                log.UserName = result.UserName;
                log.Password = result.Password;
                log.RoleName = result.Role.RoleName;

                return log;
            }
           
            return null;

        }
    }
}
