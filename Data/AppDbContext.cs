using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // These two lines will become your two database tables
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}