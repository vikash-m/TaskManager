using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain.DomainModel
{
    class LoginUserDm
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long EmpId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }        
    }
}
