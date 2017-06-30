using System.ComponentModel.DataAnnotations;
using System.IO;

namespace TaskDomain.DomainModel
{
    public class UserLoginDetails
    {

        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}
