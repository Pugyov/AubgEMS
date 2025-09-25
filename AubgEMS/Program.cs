using Microsoft.AspNetCore.Identity;
using AubgEMS.Infrastructure;          // AddInfrastructure()
using AubgEMS.Core.Constants;          // RoleNames (policies)

var builder = WebApplication.CreateBuilder(args);

// MVC + Identity UI plumbing
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Our single source of truth for DbContext + Identity (Pomelo MySQL)
builder.Services.AddInfrastructure(builder.Configuration);

// Authorization policies (used later on controllers/actions)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", p => p.RequireRole(RoleNames.Admin));
    options.AddPolicy("RequireOrganizerOrAdmin", p => p.RequireRole(RoleNames.Organizer, RoleNames.Admin));
    options.AddPolicy("RequireSignedIn", p => p.RequireAuthenticatedUser());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();