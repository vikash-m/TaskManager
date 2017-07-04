using System;
using System.Linq;
using TaskDomain.DomainModel;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class LoginRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();

        public LoginUserDm GetLoginUserDetails(string name, string password)
        {

            var response = _taskManagerEntities.LoginUsers.Where(m => m.UserName.Equals(name, StringComparison.CurrentCultureIgnoreCase) && m.Password.Equals(password)).Select(user => new LoginUserDm
            {
                Id = user.Id,
                EmpId = user.EmpId,
                UserName = user.UserName,
                Password = user.Password,
                RoleId = user.RoleId.Value
                // RoleName = GetRoleNameById(user.RoleId.Value)
            }).FirstOrDefault();
            if (response != null)
            {
                var roleName = GetRoleNameById(response.RoleId);
                response.RoleName = roleName;
            }
            return response;
        }

        public UserDetailDm GetUserDetailsData(string id)
        {
            var userDetails =
                _taskManagerEntities.UserDetails.Where(m => m.Id.Equals(id)).Select(user => new UserDetailDm
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    EmailId = user.EmailId,
                    CreateDate = user.CreateDate.Value,
                    ManagerId = user.ManagerId,
                    RoleId = user.RoleId.Value
                }).FirstOrDefault();
            if (userDetails?.RoleId != null)
            {
                var roleName = GetRoleNameById(userDetails.RoleId);
                userDetails.RoleName = roleName;
            }
            return userDetails;
        }

        public string GetEmailIfExist(string emailId)
            =>
                _taskManagerEntities.UserDetails.FirstOrDefault(x => x.EmailId.Equals(emailId) && x.IsDeleted == false)?
                    .EmailId;

        private string GetRoleNameById(int roleId) => _taskManagerEntities.Roles.FirstOrDefault(m => m.RoleId == roleId)?.RoleName;

    }
}