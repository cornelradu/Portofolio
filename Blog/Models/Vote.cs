namespace Blog.Models
{
  public partial class Vote
  {
    public int Id { get; set; }
    public int CommentaryId { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }

    public bool Up {  get; set; }

    public User User { get; set; }

    public Post Post { get; set; }

    public Commentary Commentary { get; set; }


  }
}
