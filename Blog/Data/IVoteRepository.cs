using Blog.Models;

namespace Blog
{
  public interface IVoteRepository
  {
    public bool SaveChanges();
    public void AddEntity<T>(T entityToAdd);
    public void RemoveEntity<T>(T entityToAdd);

    public void AddVote(int postId, int userId, bool up);

    public void AddVoteComment(int commentId, int userId, bool up);

  }
}
