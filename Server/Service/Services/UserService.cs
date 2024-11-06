using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess;
using BCrypt.Net;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using Service.Transfermodels.Request;
using Service.Transfermodels.Response;

namespace Service;

public class UserService : IUserService
{
    private readonly MyDbContext _context;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly IValidator<UpdateUserDto> _updateUserValidator;
    private readonly IConfiguration _configuration;

    public UserService(MyDbContext context, IValidator<CreateUserDto> createUserValidator, 
        IValidator<UpdateUserDto> updateUserValidator, IConfiguration configuration)
    {
        _context = context;
        _createUserValidator = createUserValidator;
        _updateUserValidator = updateUserValidator;
        _configuration = configuration;
    }


    public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
    {
       await _createUserValidator.ValidateAndThrowAsync(createUserDto);

       var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Passwordhash);
       
       
        var user = createUserDto.ToUser();
        user.Passwordhash = hashedPassword;
        user.Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Rolename == createUserDto.RoleName);
        if (role == null)
        {
            role = new Role { Rolename = createUserDto.RoleName };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        var userRole = new Userrole
        {
            Userid = user.Userid,
            Roleid = role.Roleid,
            Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
        };

        await _context.Userroles.AddAsync(userRole);
        await _context.SaveChangesAsync();
        
        return UserDto.FromEntity(user);
    }

    public async Task<List<UserDto>> ReadAllUsers()
    {
        var users = await _context.Users
            .Include(u => u.Userroles)
                .ThenInclude(ur => ur.Role)
            .Select(user => UserDto.FromEntity(user))
            .ToListAsync();

        return users;
    }

    public async Task<UserDto?> ReadUserById(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Userid == userId);

        return user != null ? UserDto.FromEntity(user) : null;
    }


    public async Task<UserDto> UpdateUser(int userId, UpdateUserDto updateUserDto)
    {
        await _updateUserValidator.ValidateAndThrowAsync(updateUserDto);
        
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        updateUserDto.UpdateUser(user);

        await _context.SaveChangesAsync();
        return UserDto.FromEntity(user);
    }

    public async Task<User> DeleteUser(int id)
       {
           var user = await _context.Users
               .Include(u => u.Userquestprogresses)
               .Include(u => u.Userroles)
               .Include(u => u.Scoreboards)
               .FirstOrDefaultAsync(u => u.Userid == id);
       
           if (user == null)
           {
               throw new Exception("User not found");
           }
           
           if (user.Userquestprogresses?.Any() == true)
           {
               _context.Userquestprogresses.RemoveRange(user.Userquestprogresses);
           }
       
           if (user.Userroles?.Any() == true)
           {
               _context.Userroles.RemoveRange(user.Userroles);
           }
       
           if (user.Scoreboards?.Any() == true)
           {
               _context.Scoreboards.RemoveRange(user.Scoreboards);
           }
       
           _context.Users.Remove(user);
           await _context.SaveChangesAsync();
       
           return user;
       }

    public async Task<UserDto?> Login(LoginUserDto loginUserDto)
    {
        var user = await _context.Users
            .Include(u => u.Userroles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == loginUserDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Passwordhash))
        {
            return null;
        }

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Userid.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Userroles.FirstOrDefault()?.Role.Rolename ?? "Player")
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryInMinutes"])),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        // Create a UserDto with the token
        return new UserDto
        {
            Userid = user.Userid,
            Username = user.Username,
            Email = user.Email,
            Roles = user.Userroles.Select(ur => ur.Role.Rolename).ToList(),
            Token = tokenString
        };
    }
}
