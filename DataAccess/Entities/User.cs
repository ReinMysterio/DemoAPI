using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]        
        public string Password { get; set; }

        [Required]
        public RoleType RoleType { get; set; }

        public virtual Employee Employee { get; set; }

        [Required]    
        public string RefreshToken { get; set; }
    }
}
