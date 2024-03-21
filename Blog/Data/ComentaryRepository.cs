using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Blog.Data;

namespace Blog
{
  public class ComentaryRepository : IComentaryRepository
  {
    DataContextEF _entityFramework;

    public ComentaryRepository(IConfiguration config)
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

    public IEnumerable<Commentary> GetCommentaries(int postId)
    {
        return _entityFramework.Commentaries
        .Include(p => p.User)
        .Where(p => p.PostId == postId)
        .ToList();
    }

    public void DeleteComment(int commentId)
    {
      var commentaryToDelete = _entityFramework.Commentaries.Find(commentId);
      if (commentaryToDelete != null)
      {
        _entityFramework.Commentaries.Remove(commentaryToDelete);
        _entityFramework.SaveChanges();
      }
    }


  }
}
