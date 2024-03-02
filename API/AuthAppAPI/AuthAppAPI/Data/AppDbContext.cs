using AuthAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAppAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
