using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskDomain.DomainModel
{
    public class LoginUserDm
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string RoleName { get; set; }
    }
}
