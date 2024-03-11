using DataAccess.Entities;
using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    public class UserEmployeeResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string RoleType { get; set; }

        public string Department { get; set; }
        public DateTime HireDate { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public List<ProjectResponse> Projects { get; set; }
    }
}
