using DataAccess.Context;
using DataAccess.Entities;
using DemoAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            var users = _dbContext.Users.AsNoTracking().ToList();

            foreach (var user in users)
            {
                user.Password = null;
            }

            return users;
        }

        public User GetUserById(string id)
        {
            var user = _dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Password = null;
            }
            return user;
        }

        public void AddUser(UserResquest userRequest)
        {
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
