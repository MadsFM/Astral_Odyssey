using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configuration
// Configure app configuration, logging, and any custom settings
#endregion

#region Data Access
// Configure the database context to use PostgreSQL (replace connection string)
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AstralO")));
#endregion

#region Security
// Configure Identity services
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();

// Configure application cookies (login and access denied paths)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
#endregion

#region Services
// Add other services (e.g., controllers, custom services, background services, etc.)
builder.Services.AddControllers(); // Or `AddControllersWithViews()` for MVC apps
#endregion

#region Swagger
// Add Swagger services (for API documentation if needed)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();       // Enable Swagger in development
    app.UseSwaggerUI();     // Enable Swagger UI in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

#region Security Middleware
// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
#endregion

#endregion

#region Endpoints
// Map controllers if using MVC, or define API routes
app.MapControllers(); // Use `app.MapDefaultControllerRoute();` for MVC-style route handling

app.MapGet("/", () => "Hello World!");
#endregion

app.Run();
