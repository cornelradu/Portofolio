namespace Blog.Models
{
  public partial class Commentary
  {
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string CommentText { get; set; }


    public DateTime CommentCreated { get; set; }
    public DateTime CommentUpdated { get; set; }

    public Post Post { get; set; }

    public User User { get; set; }

    public List<Vote> Votes { get; set; }


    public Commentary()
    {
      if (CommentText == null)
      {
        CommentText = "";
      }
    }
  }
}
