using Microsoft.EntityFrameworkCore;
using Simple_Blog.Model.Entities;

namespace Simple_Blog.CommonLib
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Blog>().ToTable("Blog");
        }
    }
}
