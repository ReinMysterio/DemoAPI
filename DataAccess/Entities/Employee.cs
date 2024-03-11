using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Department { get; set; }

        [Required]
        [MaxLength(50)]
        public string JobTitle { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public string UserId { get; set; }

        [ForeignKey("Id")]
        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
