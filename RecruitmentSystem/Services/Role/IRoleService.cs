using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Role;

public interface IRoleService
{

    Task<bool> addRoles(RoleModel role_type);
    Task<List<RoleModel>> getAllRoles();
    Task<bool> removeRoles(int roleId);
    Task<RoleModel> getRole(int RoleId);
}