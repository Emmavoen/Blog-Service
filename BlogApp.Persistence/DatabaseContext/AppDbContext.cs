using BlogApp.Domain.Entities;
using BlogApp.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Persistence.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
         public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());

            modelBuilder.ApplyConfiguration(new BlogConfiguration());

            modelBuilder.ApplyConfiguration(new PostConfiguration());

        }
    }
}
