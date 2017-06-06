using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDomain.DomainModel;

namespace TaskDAL.Repository
{
   public class UserRepository
    {
        TaskManagerEntities taskManagerEntities = new TaskManagerEntities();
        
        public bool SaveUser(UserdetailDm udm)
        {
            try
            {

                var Udetail = new Userdetail()
                {
                    IsDeleted = false,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    FirstName = udm.FirstName,
                    LastName = udm.LastName,
                    EmailId = udm.EmailId,
                    PhoneNumber = udm.PhoneNumber,
                    RoleId = udm.RoleId,
                    ManagerId = udm.ManagerId
                };
                taskManagerEntities.Userdetails.Add(Udetail);
                int num = taskManagerEntities.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        
        public List<Role> DropdownRoles()
        {
            var dropRoles = taskManagerEntities.Roles.ToList();
            return dropRoles; 
        }
        public List<Userdetail> DropdownMgr()
        {
            var dropMgr = taskManagerEntities.Userdetails.Where(m => m.RoleId == 3).ToList();
            return dropMgr;
        }
    }
}
