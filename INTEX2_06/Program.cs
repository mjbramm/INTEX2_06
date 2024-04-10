using INTEX2_06.Models;
using INTEX2_06.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

var googleClientId = builder.Configuration["GoogleClientId"];
var googleClientSecret = builder.Configuration["GoogleClientSecret"];

// Logging the environment variables to verify they are picked up correctly
Console.WriteLine($"Google Client ID: {builder.Configuration["GoogleClientId"]}");
Console.WriteLine($"Google Client Secret: {builder.Configuration["GoogleClientSecret"]}");

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["GoogleClientId"];
        options.ClientSecret = builder.Configuration["GoogleClientSecret"];
        if (string.IsNullOrEmpty(options.ClientId))
        {
            throw new InvalidOperationException("Google Client ID is not set.");
        }
        if (string.IsNullOrEmpty(options.ClientSecret))
        {
            throw new InvalidOperationException("Google Client Secret is not set.");
        }
    });

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
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));

builder.Services.AddTransient<ISenderEmail, EmailSender>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Set paths for login and access denied
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/InsufficientPrivileges";
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
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' 'apis.google.com' 'www.youtube.com' 's.ytimg.com' 'unsafe-inline'; " +
        "style-src 'self' 'fonts.googleapis.com' 'cdn.jsdelivr.net' 'unsafe-inline' 'www.youtube.com'; " +
        "img-src 'self' 'bwbricks.azurewebsites.net' 'www.youtube.com' 'www.lego.com' 'm.media-amazon.com' 'images.brickset.com' 'www.brickeconomy.com'; " +
        "font-src 'self' fonts.gstatic.com cdn.jsdelivr.net 'www.youtube.com'; " + 
        "connect-src 'self' 'www.youtube.com'; "); 
    await next();
});

app.MapControllerRoute("pagenumandcategory", "{legoCategory}/Page{pageNum}", new { Controller = "Home", Action = "Legostore" });
app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Legostore", pageNum = 1 });
app.MapControllerRoute("legoCategory", "{legoCategory}", new { Controller = "Home", Action = "Legostore", pageNum = 1 });
app.MapControllerRoute("pagination", "Legos/{pageNum}", new { Controller = "Home", Action = "Legostore", pageNum = 1 });



app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
