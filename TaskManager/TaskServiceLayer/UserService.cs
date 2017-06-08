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
        public List<Role> DropdownRoles()
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
    }


}
