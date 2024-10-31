
using Service.Transfermodels.Request;
using Service.Transfermodels.Response;

namespace Service.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUser(CreateUserDto createUserDto);

}