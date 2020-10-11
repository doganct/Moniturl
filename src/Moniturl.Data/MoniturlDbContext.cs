using Microsoft.EntityFrameworkCore;

namespace Moniturl.Data
{
    public class MoniturlDbContext : DbContext
    {
        public MoniturlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Target> Targets { get; set; }
        public DbSet<TargetLog> TargetLogs { get; set; }
        public DbSet<Mail> Mails { get; set; }

    }
}
