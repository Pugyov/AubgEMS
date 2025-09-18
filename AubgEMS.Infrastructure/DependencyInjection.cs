using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AubgEMS.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        var cs = cfg.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("Missing 'DefaultConnection'.");

        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));

        services.AddDefaultIdentity<IdentityUser>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}