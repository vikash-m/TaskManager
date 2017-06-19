
using System.Collections.Generic;
using TaskDAL;
using TaskDAL.Repository;
using TaskDomain.DomainModel;

namespace TaskServiceLayer
{
    public class UserService
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public bool SaveUsers(UserdetailDm userdetail)
        {
            // UserdetailDm udm = new UserdetailDm();
            var userList = _userRepository.SaveUser(userdetail);
            return userList;
        }
        public List<RoleModel> DropdownRoles()
        {
            var dropRolesRes = _userRepository.DropdownRoles();
            return dropRolesRes;
        }
        public List<Userdetail> DropdownMgr()
        {
            var dropMgrRes = _userRepository.DropdownMgr();
            return dropMgrRes;
        }
        public List<UserdetailDm> ViewUser()
        {
            var viewUsr = _userRepository.ViewUser();
            return viewUsr;
        }
        public UserdetailDm EditUser(int id)
        {
            var edit = _userRepository.EditUser(id);
            return edit;
        }
        public bool SaveEditUser(UserdetailDm udm)
        {
            var userList = _userRepository.SaveEditUser(udm);
            return userList;
        }
        public bool DeleteUser(int id)
        {
            var result = _userRepository.DeleteUser(id);
            return result;
        }
    }


}
