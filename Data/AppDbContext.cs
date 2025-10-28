using Microsoft.EntityFrameworkCore;

namespace SchedulingApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Scheduling> Schedulings { get; set; }
    }
}
