using System;
using System.Linq;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class LoginRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();


        public LoginUser GetLoginUserDetails(string name, string password)
        {
        return  _taskManagerEntities.LoginUsers.FirstOrDefault(m => m.UserName.Equals(name, StringComparison.CurrentCultureIgnoreCase) && m.Password.Equals(password, StringComparison.CurrentCultureIgnoreCase));
           
        }

        public UserDetail GetUserDetailsData(string id) => _taskManagerEntities.UserDetails.FirstOrDefault(m => m.Id.Equals(id));

        public string GetEmailIfExist(string emailId)
            =>
                _taskManagerEntities.UserDetails.FirstOrDefault(x => x.EmailId.Equals(emailId) && x.IsDeleted == false)?
                    .EmailId;

    }
}