using System.ComponentModel.DataAnnotations;

namespace DataAccess.Enums
{
    public enum RoleType
    {
        [Display(Name = "Admin")]
        Admin = 1,

        [Display(Name = "SuperAdmin")]
        SuperAdmin = 2,

        [Display(Name = "Accountant")]
        Accountant = 3,
    }
}
