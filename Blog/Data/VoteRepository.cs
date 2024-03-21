using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Blog.Data;

namespace Blog
{
  public class VoteRepository : IVoteRepository
  {
    DataContextEF _entityFramework;

    public VoteRepository(IConfiguration config)
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

    public void AddVote(int postId, int userId, bool up)
    {
      bool exists = _entityFramework.Votes.Any(v => v.PostId == postId && v.UserId == userId);
      if(exists)
      {
        Vote vote = _entityFramework.Votes.FirstOrDefault(v => v.PostId == postId && v.UserId == userId);
        if(vote.Up != up)
        {
          vote.Up = up;
          _entityFramework.SaveChanges();
        }
      } else
      {
        Vote vote = new Vote() { CommentaryId = -1, PostId = postId, UserId = userId, Up = up };
        this.AddEntity<Vote>(vote);
        _entityFramework.SaveChanges();
      }
    }

    public void AddVoteComment(int commentId, int userId, bool up)
    {
      bool exists = _entityFramework.Votes.Any(v => v.CommentaryId == commentId && v.UserId == userId);
      if (exists)
      {
        Vote vote = _entityFramework.Votes.FirstOrDefault(v => v.CommentaryId == commentId && v.UserId == userId);
        if (vote.Up != up)
        {
          vote.Up = up;
          _entityFramework.SaveChanges();
        }
      }
      else
      {
        Vote vote = new Vote() { CommentaryId = commentId, PostId = -1, UserId = userId, Up = up };
        this.AddEntity<Vote>(vote);
        _entityFramework.SaveChanges();
      }
    }

  }
}
