using System.Text.Json.Serialization;
using DataAccess;
using DataAccess.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service;
using Service.Interfaces;
using Service.Transfermodels.Request;
using Service.Validators;

var builder = WebApplication.CreateBuilder(args);

#region Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

#endregion

#region Data Access
// Configure the database context to use PostgreSQL (replace connection string)
builder.Services.AddOptionsWithValidateOnStart<AppOptions>()
    .Bind(builder.Configuration.GetSection(nameof(AppOptions)))
    .ValidateDataAnnotations()
    .Validate(options => new AppOptionsValidator().Validate(options).IsValid,
        $"{nameof(AppOptions)} validation failed");
builder.Services.AddDbContext<MyDbContext>((serviceProvider, options) =>
{
    var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>
        ().Value;
    options.UseNpgsql(Environment.GetEnvironmentVariable("AstralO") ?? appOptions.AstralO);
});
#endregion


#region Services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDto>();
builder.Services.AddOpenApiDocument(configuration =>
{
    configuration.AddTypeToSwagger<User>();
});
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

#endregion

app.Run();
