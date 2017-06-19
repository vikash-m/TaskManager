using System;
using System.Linq;
using TaskDomain.DomainModel;

namespace TaskDAL.Repository
{
    public class LoginRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();


        public LoginUserDm GetLogDetails(string name, string password)
        {
            var log = new LoginUserDm();
            var result = _taskManagerEntities.LoginUsers.FirstOrDefault(m => m.UserName == name && m.Password == password);
            if (result == null) return null;
            log.UserName = result.UserName;
            log.Password = result.Password;
            log.RoleName = result.Role.RoleName;
            log.EmpId = result.EmpId;
            log.RoleId = result.RoleId;
            return log;
        }
        public UserdetailDm GetUserDetailsData(int id)
        {
            var result = _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == id);
            var userModel = new UserdetailDm();
            try
            {
                if (result == null) return null;
                userModel.Id = result.Id;
                userModel.FirstName = result.FirstName;
                userModel.LastName = result.LastName;
                userModel.EmailId = result.EmailId;
                userModel.CreateDate = result.CreateDate;
                userModel.ModifiedDate = result.ModifiedDate;
                userModel.RoleId = result.RoleId;
                userModel.RoleName = result.Role.RoleName;
                userModel.PhoneNumber = result.PhoneNumber;
                userModel.ManagerName = _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == id) != null
                    ? _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == id)?.FirstName + " " + _taskManagerEntities.Userdetails.FirstOrDefault(m => m.Id == id)?.LastName
                    : "No Manager";

                return userModel;
            }
            catch (Exception)
            {
            }
            return null;

        }
    }
}
