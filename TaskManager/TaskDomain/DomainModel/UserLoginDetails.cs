using System.ComponentModel.DataAnnotations;
using System.IO;

namespace TaskDomain.DomainModel
{
    public class UserLoginDetails
    {

        public string EmailId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Minimum lenght of password is 6")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}
