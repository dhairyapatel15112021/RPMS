using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Role;

public class RoleServiceImpl : IRoleService{

    private readonly ApplicationDbContext _context;

    public RoleServiceImpl(ApplicationDbContext context){
        this._context = context;
    }

    public async Task<bool> addRoles(RoleModel role){
        try{
            RoleModel isRole =  await _context.Roles.FirstOrDefaultAsync(r => r.role_type == role.role_type);
            if(isRole != null){
                throw new Exception("Role Is Already Exist");
            }
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
            return false;
        }
        
    }
}