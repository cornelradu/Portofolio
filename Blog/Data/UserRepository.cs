using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFramework;

        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Remove(entityToAdd);
            }
        }

        public bool userExists(String email)
        {
            return _entityFramework.Users.Any(u => u.Email == email);
        }
        public User GetSingleUserEmail(String email)
        {
            User? user = _entityFramework.Users
               .Where(u => u.Email == email)
               .FirstOrDefault<User>();

            if (user != null)
            {
                return user;
            }

            throw new Exception("Failed to Get User");
        }

    }
}