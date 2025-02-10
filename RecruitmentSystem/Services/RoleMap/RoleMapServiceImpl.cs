using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.RoleMap;

public class RoleMapServiceImpl : IRoleMapService
{

    private readonly ApplicationDbContext _context;

    public RoleMapServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> getRoles(int empId)
    {
        try
        {
            var roles = from emp_role in _context.RolesMap join role in _context.Roles on emp_role.fk_role_id equals role.pk_role_id where emp_role.fk_emp_id == empId select role.role_type;
            return await roles.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public List<string> getRolesSync(int empId)
    {
        try
        {
            var roles = from emp_role in _context.RolesMap join role in _context.Roles on emp_role.fk_role_id 
                        equals role.pk_role_id where emp_role.fk_emp_id == empId select role.role_type;
            return roles.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<bool> mapRoles(int empId, int roleId)
    {
        try
        {
            EmpRoleMapModel role_map = new EmpRoleMapModel();
            role_map.fk_emp_id = empId;
            role_map.fk_role_id = roleId;
            await _context.RolesMap.AddAsync(role_map);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}