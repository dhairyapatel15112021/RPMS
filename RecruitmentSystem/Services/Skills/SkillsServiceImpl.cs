using System.Threading.Tasks;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Skills;

public class SkillsServiceImpl : ISkillsService
{

    private readonly ApplicationDbContext _context;

    public SkillsServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Boolean> createSkill(SkillsModel skills)
    {
        try
        {
            await _context.Skills.AddAsync(skills);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}