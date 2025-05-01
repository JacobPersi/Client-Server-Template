using Microsoft.EntityFrameworkCore;

namespace BlazorMinimalApp.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some initial data
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem { Id = 1, Title = "Learn Blazor", IsComplete = false },
                new TodoItem { Id = 2, Title = "Build a Blazor app", IsComplete = false },
                new TodoItem { Id = 3, Title = "Deploy to production", IsComplete = false }
            );
        }
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
    }
}