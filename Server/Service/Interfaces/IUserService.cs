
using DataAccess.Models;
using Service.Transfermodels.Request;
using Service.Transfermodels.Response;

namespace Service.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUser(CreateUserDto createUserDto);
    Task<List<UserDto>> ReadAllUsers();
    Task<UserDto?> ReadUserById(int userId);
    Task<UserDto> UpdateUser(int userId, UpdateUserDto updateUserDto);
    Task<User> DeleteUser(int id);

}