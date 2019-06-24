namespace Panda.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    
    public class RegisterInputModel
    {
        [Required]
        [MinLength(5), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(5), MaxLength(20)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
