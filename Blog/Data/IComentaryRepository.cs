using Blog.Models;

namespace Blog
{
  public interface IComentaryRepository
  {
    public bool SaveChanges();
    public void AddEntity<T>(T entityToAdd);
    public void RemoveEntity<T>(T entityToAdd);

    public IEnumerable<Commentary> GetCommentaries(int postId);

    public void DeleteComment(int commentId);
  }
}
