


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
  public class PostController : ControllerBase
  {
    IPostRepository _postRepository;
    IVoteRepository _voteRepository;

    IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public PostController(IConfiguration config, IPostRepository postRepository, IVoteRepository voteRepository)
    {
      _postRepository = postRepository;
      _voteRepository = voteRepository;
      _mapper = new Mapper(new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<PostDto, Post>();
      }));
    }

    [HttpPost("add_post")]
    public IActionResult AddPost(PostDto postDto)
    {
      Post postDb = _mapper.Map<Post>(postDto);
      postDb.PostCreated = DateTime.Now;
      postDb.PostUpdated = DateTime.Now;

      int userId = Int32.Parse(User.FindFirst("userId")?.Value);
      postDb.UserId = userId;

      _postRepository.AddEntity<Post>(postDb);

      if (!_postRepository.SaveChanges())
      {
        throw new Exception("Failed to Save Post");
      }
      else
      {
        return Ok();
      }

    }

    [HttpGet("list_posts")]
    public IEnumerable<dynamic> GetMyPosts(bool getAll)
    {
      if (!getAll)
      {
        int userId = Int32.Parse(User.FindFirst("userId")?.Value);
        return _postRepository.GetPosts(userId);
      } else
      {
        return _postRepository.GetPosts(-1);
      }
    }

    [HttpGet("get_post/{postId}")]
    public Post GetPostSingle(int postId)
    {
      return _postRepository.GetPostSingle(postId);
    }

    [HttpPut("edit_post/{postId}")]
    public void EditPost(int postId, PostDto postDto)
    {
      _postRepository.UpdatePostId(postId, postDto.PostTitle, postDto.PostContent);
    }

    [HttpPost("add_vote/{postId}")]
    public void AddVote(int postId, bool up)
    {
      int userId = Int32.Parse(User.FindFirst("userId")?.Value);
      _voteRepository.AddVote(postId, userId, up);
    }

    [HttpDelete("delete_post/{postId}")]
    public void DeletePost(int postId)
    {
      _postRepository.DeletePost(postId);

    }
  }
}
