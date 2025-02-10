using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public async Task<PositionSkillMapModel> getSkillPositionByPositionIdAndSkillId(int positionId, int skillId)
    {
        try
        {
            return await _context.PositionSkillMap.FirstOrDefaultAsync(p => p.fk_position_id == positionId && p.fk_skills_id == skillId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> addSkillToPosition(PositionSkillMapModel positionSkill)
    {
        try
        {
            var isAlreadyExist = await getSkillPositionByPositionIdAndSkillId(positionSkill.fk_position_id, positionSkill.fk_skills_id);
            if (isAlreadyExist != null)
            {
                throw new Exception("Skill Is Already Mapped to the position");
            }
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

    public async Task<bool> removeSkillToPosition(int positionId, int skillId)
    {
        try
        {
            var isAlreadyExist = await getSkillPositionByPositionIdAndSkillId(positionId, skillId);
            if (isAlreadyExist == null)
            {
                throw new Exception("Position and Skill Is not mapped");
            }
            await _context.PositionSkillMap.Where(p => p.pk_position_skill_id == isAlreadyExist.pk_position_skill_id).ExecuteDeleteAsync();
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