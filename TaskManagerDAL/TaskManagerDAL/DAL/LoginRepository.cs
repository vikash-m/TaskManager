using System.Linq;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class LoginRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();


        public LoginUser GetLoginUserDetails(string name, string password) => _taskManagerEntities.LoginUsers.FirstOrDefault(m => m.UserName == name && m.Password == password);


        public UserDetail GetUserDetailsData(string id) => _taskManagerEntities.UserDetails.FirstOrDefault(m => m.Id.Equals(id));

    }
}