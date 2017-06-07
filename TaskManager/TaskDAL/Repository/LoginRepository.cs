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

      
        public LoginUserDm  getLogDetails(string Name ,string Password)
        {
            LoginUserDm log = new LoginUserDm();
            var result = taskManagerEntities.LoginUsers.FirstOrDefault(m => m.UserName == Name && m.Password == Password);
            //  var res= result.Role.RoleName;
            if (result!=null)
            {
                log.UserName = result.UserName;
                log.Password = result.Password;
                log.RoleName = result.Role.RoleName;
                log.Id = result.Id;
                return log;
               
            }
           
           
            return null;

        }
        public  UserdetailDm getUserDetailsData(int Id)
        {
            var result = taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == Id);
            UserdetailDm UserModel = new UserdetailDm();
            try
            {
                UserModel.Id = result.Id;
                UserModel.FirstName = result.FirstName;
                UserModel.LastName = result.LastName;
                UserModel.EmailId = result.EmailId;
                UserModel.CreateDate = result.CreateDate;
                UserModel.ModifiedDate = result.ModifiedDate;
                UserModel.RoleName = result.Role.RoleName;
                UserModel.PhoneNumber = result.PhoneNumber;
                var ManagerName = taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == Id);
                if(ManagerName!=null)
                {
                    UserModel.ManagerName = ManagerName.FirstName + " " + ManagerName.LastName ;

                }
                else
                {
                    UserModel.ManagerName = "No Manager";
                }
                return UserModel;
            }
            catch(Exception e)
            {

            }
            return null;

        }
    }
}
