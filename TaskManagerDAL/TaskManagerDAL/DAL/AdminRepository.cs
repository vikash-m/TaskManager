using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagerDAL.Models;
using TaskDomain.DomainModel;

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
            return _taskManagerEntities.UserDetails.Where(m => m.IsDeleted != true).ToList().Select(userDetailDm => new UserDetailDm

            {
                Id = userDetailDm.Id,
                FirstName = userDetailDm.FirstName,
                LastName = userDetailDm.LastName,
                EmailId = userDetailDm.EmailId,
                PhoneNumber = userDetailDm.PhoneNumber,
                RoleId = userDetailDm.Role.RoleId,
                ManagerId = userDetailDm.ManagerId,
                CreatedBy = userDetailDm.CreatedBy,
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
            return _taskManagerEntities.Roles.FirstOrDefault(m => m.RoleId == roleId).RoleName;
        }


        public List<UserDetailDm> GetManagerByRoleId()
        {
            var result=_taskManagerEntities.UserDetails.Where(m => m.RoleId == 2).ToList();
            List<UserDetailDm> userDetailList = new List<UserDetailDm>();
            foreach (var item in result)
            {
                UserDetailDm userDetailDm = new UserDetailDm();
                userDetailDm.FirstName = item.FirstName;
                userDetailDm.Id = item.Id;
                userDetailList.Add(userDetailDm);
            }
           return userDetailList;
            
        }
        public string GetManagerNameById(string managerId)
        {
           // string name = null;
            var result = _taskManagerEntities.UserDetails.FirstOrDefault(m => m.Id == managerId)?.FirstName;
            //if (result != null)
            //{
            //    name = result.FirstName;
            //}

            //return name;
            return result;
        }

        public List<UserDetailDm> Search(string searchText)
        {
            var searchName = _taskManagerEntities.UserDetails.Where(m => m.FirstName.Contains(searchText)).ToList().Select(userDetailDm => new UserDetailDm

            {
                Id = userDetailDm.Id,
                FirstName = userDetailDm.FirstName,
                LastName = userDetailDm.LastName,
                EmailId = userDetailDm.EmailId,
                PhoneNumber = userDetailDm.PhoneNumber,
                RoleId = userDetailDm.Role.RoleId,
                ManagerId = userDetailDm.ManagerId,
                CreatedBy = userDetailDm.CreatedBy,
                ModifiedBy = userDetailDm.ModifiedBy,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ManagerName = GetManagerNameById(userDetailDm.ManagerId)

            }).ToList();


            return searchName;
        }


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
