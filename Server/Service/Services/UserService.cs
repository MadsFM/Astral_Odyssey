using DataAccess;
using BCrypt.Net;
using FluentValidation;
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
        
        return UserDto.FromEntity(user);
    }
}
