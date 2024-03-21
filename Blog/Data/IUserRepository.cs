
using Blog.Models;

namespace Blog
{
    public interface IUserRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToAdd);

        public bool userExists(String email);

        public User GetSingleUserEmail(String email);

    }
}