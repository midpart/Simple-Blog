using Microsoft.EntityFrameworkCore;
using Simple_BlogApi.Model.Entities;

namespace Simple_BlogApi.CommonLib
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
