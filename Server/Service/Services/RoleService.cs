using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Transfermodels.Request.Role;

namespace Service;

public class RoleService : IRoleService
{
    private readonly MyDbContext _context;

    public RoleService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoleCountDto>> CountRoles()
    {
        var roleCounts = await _context.Roles
            .Select(r => new RoleCountDto
            {
                RoleName = r.Rolename,
                UserCount = _context.Userroles.Count(ur => ur.Roleid == r.Roleid)
            })
            .ToListAsync();

        return roleCounts;
    }
}