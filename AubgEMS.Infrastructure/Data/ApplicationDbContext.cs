using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AubgEMS.Infrastructure.Data.Models;

namespace AubgEMS.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<EventType> EventTypes => Set<EventType>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<EventAttendance> EventAttendances => Set<EventAttendance>();
    public DbSet<Club> Clubs => Set<Club>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<NewsPost> NewsPosts => Set<NewsPost>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply all IEntityTypeConfiguration<> in this assembly
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // MySQL charset/collation (Pomelo)
        builder.UseCollation("utf8mb4_unicode_ci");
        builder.HasCharSet("utf8mb4");
    }
}