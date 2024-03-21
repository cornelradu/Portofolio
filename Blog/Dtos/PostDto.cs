using System;

namespace Blog.Dtos
{
  public partial class PostDto
  {
    public string PostTitle { get; set; }
    public string PostContent { get; set; }

    public PostDto()
    {
      if (PostTitle == null)
      {
        PostTitle = "";
      }
      if (PostContent == null)
      { 
        PostContent = "";
      }
    }
  }
}
