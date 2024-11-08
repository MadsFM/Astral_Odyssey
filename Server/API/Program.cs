using System.Text;
using System.Text.Json.Serialization;
using DataAccess;
using DataAccess.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

#region Security

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
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
    app.UseSwagger();       
    app.UseSwaggerUI();  
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseCors("AllowAll");
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
