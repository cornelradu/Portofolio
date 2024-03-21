
using Blog.Models;

namespace Blog
{
    public interface IAuthRepository
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToAdd);
        public Auth GetSingleAuth(String email);
    }
}