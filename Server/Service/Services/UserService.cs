using DataAccess;
using BCrypt.Net;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Transfermodels.Request;
using Service.Transfermodels.Response;

namespace Service;

public class UserService : IUserService
{
    private readonly MyDbContext _context;
    private readonly IValidator<CreateUserDto> _createUserValidator;

    public UserService(MyDbContext context, IValidator<CreateUserDto> createUserValidator)
    {
        _context = context;
        _createUserValidator = createUserValidator;
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

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _context.Users
            .Include(u => u.Userroles)
                .ThenInclude(ur => ur.Role)
            .Select(user => UserDto.FromEntity(user))
            .ToListAsync();

        return users;
    }

    public async Task<UserDto?> GetUserById(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Userid == userId);

        return user != null ? UserDto.FromEntity(user) : null;
    }


    public async Task<UserDto> UpdateUser(int userId, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        updateUserDto.UpdateUser(user);

        await _context.SaveChangesAsync();
        return UserDto.FromEntity(user);
    }
}
