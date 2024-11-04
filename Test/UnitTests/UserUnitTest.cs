using Bogus;
using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service;
using Service.Transfermodels.Request;
using Service.Transfermodels.Response;
using DataAccess.Models;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Test.UnitTests;

public class UserUnitTest
{
    private readonly MyDbContext _context;
    private readonly Mock<IValidator<CreateUserDto>> _mockCreateUserValidator;
    private readonly Mock<IValidator<UpdateUserDto>> _mockUpdateUserValidator;
    private readonly UserService _userService;

    public UserUnitTest()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MyDbContext(options);
        _mockCreateUserValidator = new Mock<IValidator<CreateUserDto>>();
        _mockUpdateUserValidator = new Mock<IValidator<UpdateUserDto>>();
        _userService = new UserService(_context, _mockCreateUserValidator.Object, 
            _mockUpdateUserValidator.Object);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnUserDto_WhenUserIsCreated()
    {
        //Arrange
        var faker = new Faker();
        var createdUserDto = new CreateUserDto
        {
            Username = faker.Internet.UserName(),
            Email = faker.Internet.Email(),
            Passwordhash = "Password123!",
            RoleName = "User"
        };
        
        //Act
        var result = await _userService.CreateUser(createdUserDto);
        
        //Assert
        Assert.NotNull(result);
        Assert.IsType<UserDto>(result);
        Assert.Equal(createdUserDto.Username, result.Username);
        Assert.Equal(createdUserDto.Email, result.Email);
        Assert.NotEqual(default, result.Userid);
        Assert.NotNull(result.Createdat);
    }

    [Fact]
    public async Task UpdateUser_ShouldUpdateUserInfo_WhenDataIsValid()
    {
        //Arrange
        var initUser = new User
        {
            Userid = 1,
            Username = "originalUser",
            Email = "original@example.com",
            Passwordhash = BCrypt.Net.BCrypt.HashPassword("OriginalPasswordHash123!"),
            Createdat = DateTime.UtcNow
        };

        await _context.Users.AddAsync(initUser);
        await _context.SaveChangesAsync();

        _mockUpdateUserValidator
            .Setup(v => v.ValidateAsync(It.IsAny<UpdateUserDto>(), default))
            .ReturnsAsync(new ValidationResult());

        var updateUserDto = new UpdateUserDto
        {
            Username = "updatedUser",
            Email = "updated@example.com",
            NewPassword = "OriginalPasswordHash123!"
        };
        
        //Act
        var result = await _userService.UpdateUser(initUser.Userid, updateUserDto);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(updateUserDto.Username, result.Username);
        Assert.Equal(updateUserDto.Email, result.Email);

        var updatedUserInMemo = await _context.Users.FindAsync(initUser.Userid);
        Assert.NotNull(updatedUserInMemo);
        Assert.Equal(updateUserDto.Username, updatedUserInMemo.Username);
        Assert.Equal(updateUserDto.Email, updatedUserInMemo.Email);
        //testing for if password is hashed
        Assert.True(BCrypt.Net.BCrypt.Verify(updateUserDto.NewPassword, updatedUserInMemo.Passwordhash));
    }
}