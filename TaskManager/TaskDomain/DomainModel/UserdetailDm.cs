using System;
using System.ComponentModel.DataAnnotations;

namespace TaskDomain.DomainModel
{
    public class UserDetailDm
    {
        public string Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter the First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter the Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone no")]
        [Required(ErrorMessage = "Enter the Phone no")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]

        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Email Id")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid email id")]
        public string EmailId { get; set; }
        public int? RoleId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string RoleName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerId { get; set; }
    }
}
