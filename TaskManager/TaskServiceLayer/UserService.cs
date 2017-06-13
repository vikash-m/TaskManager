using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDAL;
using TaskDAL.Repository;
using TaskDomain.DomainModel;

namespace TaskServiceLayer
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();
        TaskManagerEntities taskManagerEntities = new TaskManagerEntities();

        public bool SaveUsers(UserdetailDm udm)
        {
           // UserdetailDm udm = new UserdetailDm();
            bool UserList = userRepository.SaveUser(udm);
            return UserList;
        }
        public List<RoleModel> DropdownRoles()
        {
            var dropRolesRes = userRepository.DropdownRoles();
            return dropRolesRes;
        }
        public List<Userdetail> DropdownMgr()
        {
            var dropMgrRes = userRepository.DropdownMgr();
            return dropMgrRes;
        }
        public List<UserdetailDm> ViewUser()
        {
            var viewUsr = userRepository.ViewUser();
            return viewUsr;
        }
        public UserdetailDm EditUser(int id)
        {
           var edit= userRepository.EditUser(id);
            return edit;
        }
        public bool SaveEditUser(UserdetailDm udm)
        {
            bool UserList = userRepository.SaveEditUser(udm);
            return UserList;
        }
        public Userdetail DeleteUser(int id)
        {
            var del = userRepository.DeleteUser(id);
            return del;
        }
    }


}
