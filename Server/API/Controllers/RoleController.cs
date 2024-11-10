using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Transfermodels.Request.Role;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;


    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Route("role-count")]
    public async Task<ActionResult<List<RoleCountDto>>> GetRoleCount()
    {
        var roleCount = await _roleService.CountRoles();
        return Ok(roleCount);
    }
}