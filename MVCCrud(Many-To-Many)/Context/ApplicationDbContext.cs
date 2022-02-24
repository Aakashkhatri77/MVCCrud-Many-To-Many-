using Microsoft.EntityFrameworkCore;
using MVCCrud_Many_To_Many_.Models;

namespace MVCCrud_Many_To_Many_.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCategory>().HasKey(pc => new { pc.PostId, pc.CategoryId });
            modelBuilder.Entity<Post>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<Category>().Property(p=> p.Title).IsRequired();
        }


    }
}
