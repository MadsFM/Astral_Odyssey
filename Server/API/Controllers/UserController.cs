using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Transfermodels.Request;
using Service.Transfermodels.Response;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = await _userService.CreateUser(createUserDto);
        return CreatedAtAction(nameof(CreateUser), new { id = createdUser.Userid }, createdUser);
    }
    
    
}