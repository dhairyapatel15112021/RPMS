using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Role;

public class RoleServiceImpl : IRoleService
{

    private readonly ApplicationDbContext _context;

    public RoleServiceImpl(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<bool> addRoles(RoleModel role)
    {
        try
        {
            RoleModel isRole = await _context.Roles.FirstOrDefaultAsync(r => r.role_type == role.role_type);
            if (isRole != null)
            {
                throw new Exception("Role Is Already Exist");
            }
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }

    public async Task<List<RoleModel>> getAllRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<bool> removeRoles(int roleId)
    {
        try
        {
            RoleModel is_role_exist = await getRole(roleId);
            if (is_role_exist == null)
            {
                throw new Exception("Role Does not exist");
            }
            await _context.Roles.Where(r => r.pk_role_id == roleId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<RoleModel> getRole(int RoleId)
    {
        return await _context.Roles.FindAsync(RoleId);
    }
}