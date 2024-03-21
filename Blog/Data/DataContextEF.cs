
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Auth> Auths { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Commentary> Commentaries { get; set; }

        public virtual DbSet<Vote> Votes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                        optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Blog");

            modelBuilder.Entity<User>()
                .ToTable("Users", "Blog")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Auth>()
                .ToTable("Auth", "Blog")
                .HasKey(u => u.Email);

            modelBuilder.Entity<Post>()
              .ToTable("Posts", "Blog")
              .HasKey(u => u.PostId);

            modelBuilder.Entity<Post>()
                  .HasOne(p => p.User)
                  .WithMany()
                  .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Commentary>()
              .ToTable("Commentary", "Blog")
              .HasKey(u => u.Id);

            modelBuilder.Entity<Commentary>()
              .HasOne(c => c.Post)  
              .WithMany(p => p.Commentaries) 
              .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Vote>()
              .HasOne(c => c.Post)
              .WithMany(p => p.Votes)
              .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Vote>()
                    .ToTable("Vote", "Blog")
                    .HasKey(u => u.Id);

      modelBuilder.Entity<Vote>()
          .HasOne(c => c.Commentary)
          .WithMany(p => p.Votes)
          .HasForeignKey(c => c.CommentaryId);
    }
    }
}
