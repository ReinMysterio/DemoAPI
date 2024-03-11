using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    public class UserResquest
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format. Please provide a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Compare("Email", ErrorMessage = "Confirm Email must match the email.")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "RoleType is required.")]
        public RoleType RoleType { get; set; }
    }
}
