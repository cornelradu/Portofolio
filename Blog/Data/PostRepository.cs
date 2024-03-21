using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Blog.Data;

namespace Blog
{
  public class PostRepository : IPostRepository
  {
    DataContextEF _entityFramework;

    public PostRepository(IConfiguration config)
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

    public IEnumerable<dynamic> GetPosts(int userId)
    {
      if (userId == -1)
      {
        return _entityFramework.Posts
          .Include(p => p.User)
          .Select(p => new
          {
            Post = p,
            Commentaries = p.Commentaries.Select(c => new
            {
              c.Id,
              c.CommentText,
              c.CommentCreated,
              c.CommentUpdated,
              c.User,
              c.Votes
            }).ToList(),
            Votes = p.Votes.Select(c => new
            {
              c.Id,
              c.Up
            }).ToList()
          })
          .ToList();
      } else
      {
        return _entityFramework.Posts
          .Include(p => p.User)
          .Where(p => p.UserId == userId)
          .Select(p => new
          {
            Post = p,
            Commentaries = p.Commentaries.Select(c => new
            {
              c.Id,
              c.CommentText,
              c.CommentCreated,
              c.CommentUpdated,
              c.User,
              c.Votes
            }).ToList(),
            Votes = p.Votes.Select(c => new
            {
              c.Id,
              c.Up
            }).ToList()
          })
          .ToList();
      }
    }

    public Post GetPostSingle(int postId)
    {
      return _entityFramework.Posts
      .Include(p => p.User)
      .Where(p => p.PostId == postId)
      .First<Post>();

    }
    public void UpdatePostId(int postId, String title, String content)
    {
      Post postDb = _entityFramework.Posts.Where(p => p.PostId == postId).First<Post>();
      postDb.PostTitle = title;
      postDb.PostContent = content;
      postDb.PostUpdated = DateTime.Now;

      _entityFramework.SaveChanges();
    }

    public void DeletePost(int postId)
    {
      var postToDelete = _entityFramework.Posts.Find(postId);
      if (postToDelete != null)
      {
        _entityFramework.Posts.Remove(postToDelete);
        _entityFramework.SaveChanges();
      }
    }

  }
}
