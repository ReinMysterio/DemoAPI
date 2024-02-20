using DemoAPI.Models;

namespace DemoAPI.Services
{
    public class UserService
    {
        private List<User> _users;

        public UserService()
        {
            _users = new List<User>
            {
                new User { Id = "de7d268d-7cd7-4b76-8985-d7a56c751f8a", UserName = "user1", Name = "User One", Email = "user1@test.com" },
                new User { Id = "4ff45f84-8838-4cc3-ae3a-5ebca643824d", UserName = "user2", Name = "User Two", Email = "user2@test.com" },
                new User { Id = "4a915d1c-e51d-4ed8-9332-c507cc1037a2", UserName = "user3", Name = "User Three", Email = "user3@test.com" },
                new User { Id = "89ed3a7f-a9d4-4162-88a0-7e737cfbe548", UserName = "user4", Name = "User Four", Email = "user4@test.com" },
                new User { Id = "74f83b1b-f029-493b-80ca-64caf4bdc3f3", UserName = "user5", Name = "User Five", Email = "user5@test.com" }
            };
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserById(string id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _users.Add(user);
        }

        public bool UpdateUser(string id, User user) 
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null) 
            {
                existingUser.UserName =  user.UserName;
                existingUser.Email = user.Email;
                existingUser.ConfirmEmail = user.ConfirmEmail;

                return true;
            }

            return false;
        }

        public bool DeleteUser(string id) 
        {
            var deleteUser = _users.FirstOrDefault(u => u.Id == id);
            if (deleteUser != null) 
            {
                return _users.Remove(deleteUser);
            }

            return false;
        }
    }
}
