using Microsoft.EntityFrameworkCore;
using OrderlyticsAuth.Model;

namespace OrderlyticsAuth
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
