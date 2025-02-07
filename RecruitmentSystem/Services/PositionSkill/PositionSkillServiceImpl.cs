using System.Threading.Tasks;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.PositionSkill;

public class PositionSkillServiceImpl : IPositionSkillService
{

    private ApplicationDbContext _context;

    public PositionSkillServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> addSkillToPosition(PositionSkillMapModel positionSkill)
    {
        try
        {
            await _context.PositionSkillMap.AddAsync(positionSkill);
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