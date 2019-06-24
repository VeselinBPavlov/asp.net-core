namespace Panda.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [Required]
        [MinLength(5), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
