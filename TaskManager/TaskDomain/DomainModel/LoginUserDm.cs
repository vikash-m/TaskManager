using System;
using System.ComponentModel.DataAnnotations;

namespace TaskDomain.DomainModel
{
    public class LoginUserDm
    {
        public string Id { get; set; }
        public int RoleId { get; set; }
        public int EmpId { get; set; }

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
