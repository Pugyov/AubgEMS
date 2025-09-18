using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AubgEMS.Infrastructure.Data;

public class DesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var cs = "Server=localhost;Port=3306;Database=EventDB;User ID=event_user;Password=StrongPassword123!;SslMode=None;AllowPublicKeyRetrieval=True;CharSet=utf8mb4";
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseMySql(cs, ServerVersion.AutoDetect(cs))
            .Options;
        return new ApplicationDbContext(options);
    }
}