namespace Blog.Models
{
    public partial class Auth
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public Auth(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}