using Blog.Models;

namespace Blog
{
  public interface IPostRepository
  {
    public bool SaveChanges();
    public void AddEntity<T>(T entityToAdd);
    public void RemoveEntity<T>(T entityToAdd);

    public IEnumerable<dynamic> GetPosts(int userId);

    public Post GetPostSingle(int postId);

    public void UpdatePostId(int postId, String title, String content);

    public void DeletePost(int postId);
 
  }
}
