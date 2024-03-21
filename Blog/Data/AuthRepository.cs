using Blog.Models;

namespace Blog.Data
{
    public class AuthRepository : IAuthRepository
    {
        DataContextEF _entityFramework;

        public AuthRepository(IConfiguration config)
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

        public Auth GetSingleAuth(String email)
        {
            Auth? auth = _entityFramework.Auths
                .Where(u => u.Email == email)
                .FirstOrDefault<Auth>();

            if (auth != null)
            {
                return auth;
            }

            throw new Exception("Failed to Get User");
        }
    }
}