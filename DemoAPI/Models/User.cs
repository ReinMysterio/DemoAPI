using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Mali ang email format mo.")]

        public string Email { get; set; }

        public string Password { get; set; }

        public string EmailConfirmed { get; set; }
    }
}
