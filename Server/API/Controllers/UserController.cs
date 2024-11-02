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

    [HttpGet("getAll")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);

        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }
        return Ok(user);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updateUser = await _userService.UpdateUser(userId, updateUserDto);
            return Ok(updateUser);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    
}