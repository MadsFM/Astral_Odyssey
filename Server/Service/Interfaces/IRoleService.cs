
using Service.Transfermodels.Request.Role;


namespace Service.Interfaces;

public interface IRoleService
{
    Task<List<RoleCountDto>> CountRoles();
}