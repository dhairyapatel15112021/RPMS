using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<SkillsModel>> getAllSkills()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<bool> updateSkill(int id, SkillsModel skills)
    {
        try
        {
            var skillToUpdate = await _context.Skills.FindAsync(id);
            if (skillToUpdate is null)
            {
                throw new ArgumentNullException("Invalid Skill Id");
            }
            await _context.Skills.Where(s => s.pk_skills_id == skillToUpdate.pk_skills_id).ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.is_min_req_skills, skills.is_min_req_skills)
            .SetProperty(p => p.skills_description, skills.skills_description)
            .SetProperty(p => p.skills_name, skills.skills_name));
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