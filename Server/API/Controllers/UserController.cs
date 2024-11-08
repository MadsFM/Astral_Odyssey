using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        createUserDto.RoleName = "Player";

        var createdUser = await _userService.CreateUser(createUserDto);
        return CreatedAtAction(nameof(CreateUser), new { id = createdUser.Userid }, createdUser);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("getAll")]
    public async Task<ActionResult<List<UserDto>>> ReadAllUsers()
    {
        var users = await _userService.ReadAllUsers();
        return Ok(users);
    }

    [Authorize(Roles = "Admin, Player")]
    [HttpGet("getById/{id}")]
    public async Task<ActionResult<UserDto>> ReadUserById(int id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (User.IsInRole("Player") && currentUserId != id.ToString())
        {
            return Forbid("Players can only view their own profile!");
        }
        
        var user = await _userService.ReadUserById(id);

        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }
        return Ok(user);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserDto loginUserDto)
    {
        var userDto = await _userService.Login(loginUserDto);

        if (userDto == null)
        {
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        return Ok(userDto);
    }

    [Authorize(Roles = "Admin, Player")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Player") && currentUserId != id.ToString())
            {
                return Forbid("Players can only delete their own account!");
            }
            
            var updateUser = await _userService.UpdateUser(id, updateUserDto);
            return Ok(updateUser);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = "Admin, Player")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> DeleteUser(int id)
    {
        try
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Player") && currentUserId != id.ToString())
            {
                return Forbid("Players can only delete their own account!");
            }
            var deletedUser = await _userService.DeleteUser(id);
            return Ok(UserDto.FromEntity(deletedUser));
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}