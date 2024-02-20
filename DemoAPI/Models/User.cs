using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    public class User
    {
        [Required(ErrorMessage = "Id is required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format. Please provide a valid email address.")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Email", ErrorMessage = "Confirm Email must match the email.")]
        public string ConfirmEmail { get; set; }
    }
}
