using INTEX2_06.Models;
using INTEX2_06.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;






// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<LegostoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILegoRepository, EFLegoRepository>();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 4;
        // Other settings can be configured here

    })
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders(); builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));

builder.Services.AddTransient<ISenderEmail, EmailSender>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // If the LoginPath isn't set, ASP.NET Core defaults the path to /Account/Login.
    options.LoginPath = "/Account/Login"; // Set your login path here
    options.AccessDeniedPath = "/Account/InsufficientPrivileges"; // Set the path to the page for insufficient privileges
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    // Set token lifespan to 2 hours
    options.TokenLifespan = TimeSpan.FromHours(2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS vall is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute("pagenumandcategory", "{legoCategory}/Page{pageNum}", new { Controller = "Home", Action = "Legostore" });
app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Legostore", pageNum = 1 });
app.MapControllerRoute("legoCategory", "{legoCategory}", new { Controller = "Home", Action = "Legostore", pageNum = 1 });
app.MapControllerRoute("pagination", "Legos/{pageNum}", new { Controller = "Home", Action = "Legostore", pageNum = 1 });

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
