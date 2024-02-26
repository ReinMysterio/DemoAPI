using DataAccess.Context;
using DataAccess.Entities;
using DemoAPI.Models;

namespace DemoAPI.Services
{
    public class UserService
    {
        private readonly DemoAPIContext _dbContext;
        private List<UserResquest> _users;

        public UserService(DemoAPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(string id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(UserResquest userRequest)
        {
            var toAdd = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userRequest.UserName,
                Name = userRequest.Name,
                Email = userRequest.Email,
                Password = userRequest.Password
            };
            _dbContext.Users.Add(toAdd);
            _dbContext.SaveChanges();
        }

        public bool UpdateUser(string id, UserResquest user) 
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null) 
            {
                existingUser.UserName =  user.UserName;
                existingUser.Email = user.Email;
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;

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
            var deleteUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (deleteUser != null) 
            {
                _dbContext.Users.Remove(deleteUser);
                return _dbContext.SaveChanges() > 0;

            }

            return false;
        }
    }
}
