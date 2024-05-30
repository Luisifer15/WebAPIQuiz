using System.ComponentModel.DataAnnotations;

namespace WebAPIQuiz.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Username required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
