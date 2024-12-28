using Microsoft.EntityFrameworkCore;

namespace Street.Healing.API.Context
{
    public class UserDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
