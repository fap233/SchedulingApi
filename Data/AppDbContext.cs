using Microsoft.EntityFrameworkCore;
using SchedulingApi.Data;

namespace SchedulingApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Scheduling> Schedulings { get; set; }
    }
}

public class Scheduling
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Schedule { get; set; }
}
