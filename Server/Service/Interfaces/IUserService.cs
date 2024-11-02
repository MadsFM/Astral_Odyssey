
using DataAccess.Models;
using Service.Transfermodels.Request;
using Service.Transfermodels.Response;

namespace Service.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUser(CreateUserDto createUserDto);
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto?> GetUserById(int userId);
    Task<UserDto> UpdateUser(int userId, UpdateUserDto updateUserDto);

}