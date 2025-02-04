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

    public bool addSkillToPosition(PositionSkillMapModel positionSkill)
    {
        try
        {
            _context.PositionSkillMap.Add(positionSkill);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}