using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskDomain.DomainModel;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class AdminRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();
        private readonly ManagerRepository _managerRepository = new ManagerRepository();

        public bool CreateUser(UserDetail userDetail)
        {
            _taskManagerEntities.UserDetails.Add(userDetail);
            var num = _taskManagerEntities.SaveChanges();
            return num > 0;

        }

        public List<UserDetailDm> GetUser() => _taskManagerEntities.UserDetails.Where(m => m.IsDeleted != true)
            .ToList().Select(user => new UserDetailDm
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailId = user.EmailId,
                ManagerName = _managerRepository.GetEmployeeNameById(user.ManagerId),
                ManagerId = user.ManagerId,
                RoleName = GetRoleById(user.RoleId),
                RoleId = user.RoleId
            }).ToList();

        public bool CreateLoginUser(LoginUser loginUser)
        {
            _taskManagerEntities.LoginUsers.Add(loginUser);
            var saveStatus = _taskManagerEntities.SaveChanges();
            return saveStatus > 0;

        }

        public List<Role> GetRoles() => _taskManagerEntities.Roles.ToList();

        public string GetRoleById(int? roleId)
        {
            return _taskManagerEntities.Roles.FirstOrDefault(m => m.RoleId == roleId)?.RoleName;
        }

        public List<UserDetail> GetManagerByRoleId(int roleId) => _taskManagerEntities.UserDetails.Where(m => m.RoleId == roleId).ToList();

        public UserDetail GetUserDetailById(string id) => _taskManagerEntities.UserDetails.FirstOrDefault(x => x.Id == id);

        public bool UpdateUserDetail(UserDetail userdetail)
        {
            try
            {
                var userDetail = _taskManagerEntities.UserDetails.FirstOrDefault(m => m.Id == userdetail.Id);
                if (userDetail == null)
                {
                    return false;
                }
                userDetail.Id = userdetail.Id;
                userDetail.FirstName = userdetail.FirstName;
                userDetail.LastName = userdetail.LastName;
                userDetail.EmailId = userdetail.EmailId;
                userDetail.PhoneNumber = userdetail.PhoneNumber;
                userDetail.RoleId = userdetail.RoleId;
                userDetail.ManagerId = userdetail.ManagerId;
                userDetail.ModifiedDate = DateTime.Now;
                _taskManagerEntities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteUser(string id)
        {
            var userDetail = _taskManagerEntities.UserDetails.Find(id);

            if (userDetail == null)
            {
                return false;
            }
            userDetail.IsDeleted = true;
            userDetail.ModifiedDate = DateTime.Now;
            _taskManagerEntities.SaveChanges();
            return true;
        }

        public UserDetail GetUserDetailByEmailId(string emailId) => _taskManagerEntities.UserDetails.First(r => r.EmailId == emailId);

    }
}