using System.ComponentModel.DataAnnotations;


namespace TaskDomain.DomainModel
{
    public class UserLoginDetails
    {

        public string EmailId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Minimum lenght of password is 8")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}
