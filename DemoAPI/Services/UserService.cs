using DataAccess.Context;
using DataAccess.Entities;
using DemoAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DemoAPI.Services
{
    public class UserService
    {
        // For Entity Framework
        private readonly DemoAPIContext _dbContext;        

        public UserService(DemoAPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var usersWithEmployee = _dbContext.Users.AsNoTracking().ToList();

            foreach (var user in usersWithEmployee)
            {
                user.Password = null;
            }

            return usersWithEmployee;
        }

        public UserEmployeeResponse GetUserById(string id)
        {
            var user = _dbContext.Users
                .Include(t => t.Employee)
                    .ThenInclude(t => t.Projects)
                .FirstOrDefault(u => u.Id == id);


            if (user == null)
            {
                return null;              
            }

            return new UserEmployeeResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Department = user.Employee.Department,
                HireDate = user.Employee.HireDate,
                JobTitle = user.Employee.JobTitle,
                Salary = user.Employee.Salary,
                RoleType = user.RoleType.GetDisplayName(),
                Projects = user.Employee.Projects.Select(p => new ProjectResponse
                {
                    ProjectId = p.Id,
                    ProjectName = p.Name,
                    ProjectStartDate = p.StartDate,
                    ProjectEndDate = p.EndDate
                }).ToList()

            };
        }

        public void AddUser(UserResquest userRequest)
        {
            var hasExistingUser = _dbContext.Users.Any(u => u.UserName == userRequest.UserName);
            if (hasExistingUser)
            {
                throw new Exception("User already exists");
            }

            var toAdd = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userRequest.UserName,
                Name = userRequest.Name,
                Email = userRequest.Email,
                Password = BCryptNet.HashPassword(userRequest.Password, BCryptNet.GenerateSalt(12))
            };
            _dbContext.Users.Add(toAdd);
            _dbContext.SaveChanges();
        }

        public bool UpdateUser(string id, UserResquest userRequest) 
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.UserName = userRequest.UserName;
                existingUser.Email = userRequest.Email;
                existingUser.Name = userRequest.Name;
                existingUser.Password = BCryptNet.HashPassword(userRequest.Password, BCryptNet.GenerateSalt(12));

                return _dbContext.SaveChanges() > 0;
            }

            return false;
        }

        public bool UpdateEmail(string id, string email)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Email = email;

                return _dbContext.SaveChanges() > 0;
            }

            return false;
        }


        public bool DeleteUser(string id) 
        {
            var deleteuser = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (deleteuser != null)
            {
                _dbContext.Users.Remove(deleteuser);
                return _dbContext.SaveChanges() > 0;

            }

            return false;
        }
    }
}
