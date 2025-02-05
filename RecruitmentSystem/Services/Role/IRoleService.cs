using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Role;

public interface IRoleService{

    Task<bool> addRoles(RoleModel role_type);
}