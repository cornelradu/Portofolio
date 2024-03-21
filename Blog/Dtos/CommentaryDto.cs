using System;

namespace Blog.Dtos
{
  public partial class CommentaryDto
  {
    public string CommentText { get; set; }

    public int PostId { get; set; }

    public CommentaryDto()
    {
      if (CommentText == null)
      {
        CommentText = "";
      }
    }
  }
}
