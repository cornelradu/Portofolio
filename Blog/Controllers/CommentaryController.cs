

using Microsoft.AspNetCore.Mvc;
using Blog.Dtos;
using AutoMapper;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Dapper;
using System.Data;
namespace Blog.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class CommentaryController : ControllerBase
  {
    IComentaryRepository _commentaryRepository;
    IVoteRepository _voteRepository;
    IMapper _mapper;

    public CommentaryController(IConfiguration config, IComentaryRepository comentaryRepository, IVoteRepository voteRepository)
    {
      _commentaryRepository = comentaryRepository;
      _mapper = new Mapper(new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<CommentaryDto, Commentary>();
      }));
      _voteRepository = voteRepository;
    }

    [HttpPost("add_commentary")]
    public IActionResult AddComment(CommentaryDto commentaryDto)
    {
      Commentary commentDb = _mapper.Map<Commentary>(commentaryDto);
      commentDb.CommentCreated = DateTime.Now;
      commentDb.CommentUpdated = DateTime.Now;

      int userId = Int32.Parse(User.FindFirst("userId")?.Value);
      commentDb.UserId = userId;

      _commentaryRepository.AddEntity<Commentary>(commentDb);

      if (!_commentaryRepository.SaveChanges())
      {
        throw new Exception("Failed to Save Post");
      }
      else
      {
        return Ok();
      }

    }

    [HttpGet("list_comments/{postId}")]
    public IEnumerable<Commentary> GetCommentaries(int postId)
    {
        return _commentaryRepository.GetCommentaries(postId);

    }

    [HttpDelete("delete_commentary/{commentId}")]
    public void DeleteComment(int commentId)
    {
        _commentaryRepository.DeleteComment(commentId);

    }

    [HttpPost("add_vote/{commentId}")]
    public void AddVote(int commentId, bool up)
    {
      int userId = Int32.Parse(User.FindFirst("userId")?.Value);
      _voteRepository.AddVoteComment(commentId, userId, up);
    }
  }
}
