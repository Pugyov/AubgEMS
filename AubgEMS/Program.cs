using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AubgEMS.Infrastructure;
using AubgEMS.Infrastructure.Data;
using AubgEMS.Core.Constants; 

var builder = WebApplication.CreateBuilder(args);

// MVC + Identity UI
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// DbContext + Identity registration lives here
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor(); 

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin",
        policy => policy.RequireRole(RoleNames.Admin));

    options.AddPolicy("RequireOrganizerOrAdmin",
        policy => policy.RequireRole(RoleNames.Organizer, RoleNames.Admin));

    options.AddPolicy("RequireSignedIn",
        policy => policy.RequireAuthenticatedUser());
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity UI pages
app.MapRazorPages();

app.Run();