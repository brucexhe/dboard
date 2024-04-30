using Microsoft.EntityFrameworkCore;

namespace Dboard.Db
{
    public class SqliteDbContext : DbContext
    { 

        public SqliteDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
