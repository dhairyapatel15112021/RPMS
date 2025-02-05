namespace RecruitmentSystem.Services.RoleMap;

public interface IRoleMapService{
    Task<bool> mapRoles(int empId,int roleId);
    Task<List<string>> getRoles(int empId);
    List<string> getRolesSync(int empId);
}
