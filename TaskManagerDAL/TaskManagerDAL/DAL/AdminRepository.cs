using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using TaskDomain.DomainModel;
using TaskManagerDAL.Models;

namespace TaskManagerDAL.DAL
{
    public class AdminRepository
    {
        private readonly TaskManagerEntities _taskManagerEntities = new TaskManagerEntities();

        public bool CreateUser(UserDetail userDetail)
        {
            _taskManagerEntities.UserDetails.Add(userDetail);
            var num = _taskManagerEntities.SaveChanges();
            return num > 0;
        }

        public List<UserDetailDm> GetUser()
        {
            return
                _taskManagerEntities.UserDetails.Where(m => m.IsDeleted != true)
                    .ToList()
                    .Select(userDetailDm => new UserDetailDm
                    {
                        Id = userDetailDm.Id,
                        FirstName = userDetailDm.FirstName,
                        LastName = userDetailDm.LastName,
                        EmailId = userDetailDm.EmailId,
                        PhoneNumber = userDetailDm.PhoneNumber,
                        RoleId = userDetailDm.Role.RoleId,
                        ManagerId = userDetailDm.ManagerId,
                        ModifiedBy = userDetailDm.ModifiedBy,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        ManagerName = GetManagerNameById(userDetailDm.ManagerId)
                    }).ToList();
        }

        public bool CreateLoginUser(LoginUser loginUser)
        {
            _taskManagerEntities.LoginUsers.Add(loginUser);
            var saveStatus = _taskManagerEntities.SaveChanges();
            return saveStatus > 0;
        }

        public List<RoleDm> GetRoles() => _taskManagerEntities.Roles.ToList().Select(roles => new RoleDm
        {
            RoleId = roles.RoleId,
            RoleName = roles.RoleName
        }).ToList();

        public string GetRoleNameById(int roleId)
        {
            return _taskManagerEntities.Roles.FirstOrDefault(m => m.RoleId == roleId)?.RoleName;
        }

        public List<UserDetailDm> GetManagerByRoleId()
        {
            var roleId = Convert.ToInt32(EnumClass.Roles.Manager);
            var result = _taskManagerEntities.UserDetails.Where(m => m.RoleId == roleId).ToList();
            var userDetailList = new List<UserDetailDm>();
            foreach (var item in result)
            {
                var userDetailDm = new UserDetailDm()
                {
                    FirstName = item.FirstName,
                    Id = item.Id
                };
                userDetailList.Add(userDetailDm);
            }
            return userDetailList;
        }

        public string GetManagerNameById(string managerId)
        {
            var result = _taskManagerEntities.UserDetails.FirstOrDefault(m => m.Id == managerId)?.FirstName;
            return result;
        }

        public UserDetail GetUserDetailById(string id)
            => _taskManagerEntities.UserDetails.FirstOrDefault(x => x.Id == id);

        public bool UpdateUserDetail(UserDetail userdetail)
        {
            try
            {
                var userDetail = _taskManagerEntities.UserDetails.FirstOrDefault(m => m.Id == userdetail.Id);
                if (userDetail == null)
                    return false;
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
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(string id)
        {
            var userDetail = _taskManagerEntities.UserDetails.Find(id);

            if (userDetail == null)
                return false;
            userDetail.IsDeleted = true;
            userDetail.ModifiedDate = DateTime.Now;
            _taskManagerEntities.SaveChanges();
            return true;
        }

        public UserDetail GetUserDetailByEmailId(string emailId)
            => _taskManagerEntities.UserDetails.First(r => r.EmailId == emailId);

        public bool CheckForEmail(string email) => _taskManagerEntities.UserDetails.FirstOrDefault(x => x.EmailId.Equals(email) && x.IsDeleted == false) == null;



    }
}