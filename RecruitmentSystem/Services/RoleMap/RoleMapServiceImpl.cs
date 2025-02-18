using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.RoleMap;

public class RoleMapServiceImpl : IRoleMapService
{

    private readonly ApplicationDbContext _context;

    public RoleMapServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<EmployeeReviewerdto> getEmployeeRoles(string filterRole)
    {
        try
        {
            var result = from emp in _context.Employees join rolemap in _context.RolesMap on emp.pk_emp_id equals rolemap.fk_emp_id join role in _context.Roles on rolemap.fk_role_id equals role.pk_role_id where role.role_type == filterRole select new { emp };
            List<EmployeeReviewerdto> reviewers = new List<EmployeeReviewerdto>();
            foreach (var r in result)
            {
                EmployeeReviewerdto dto = new EmployeeReviewerdto();

                dto.emp_name = r.emp.emp_name;
                dto.pk_emp_id = r.emp.pk_emp_id;

                reviewers.Add(dto);
            }
            return reviewers;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
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
            var roles = from emp_role in _context.RolesMap
                        join role in _context.Roles on emp_role.fk_role_id
                        equals role.pk_role_id
                        where emp_role.fk_emp_id == empId
                        select role.role_type;
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