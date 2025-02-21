using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.PositionSkill;

public class PositionSkillServiceImpl : IPositionSkillService
{

    private ApplicationDbContext _context;

    public PositionSkillServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> addSkillToWhilePositionCreation(PositionSkilldto positionSkilldto)
    {
        try
        {
            var newMinSkills = positionSkilldto.minimumSkill.Select(id => new PositionSkillMapModel { fk_position_id = positionSkilldto.positionId, fk_skills_id = id, is_min_req_skills = true });
            var preferedSkills = positionSkilldto.preferedSkill.Select(id => new PositionSkillMapModel { fk_position_id = positionSkilldto.positionId, fk_skills_id = id, is_min_req_skills = false });
            await _context.PositionSkillMap.AddRangeAsync(newMinSkills);
            await _context.PositionSkillMap.AddRangeAsync(preferedSkills);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> addSkillToPosition(PositionSkilldto positionSkilldto)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            Console.WriteLine("hi - 3");
            var allIds = positionSkilldto.minimumSkill.Concat(positionSkilldto.preferedSkill);

            var existingSkills = await _context.PositionSkillMap.Where(s => allIds.Contains(s.fk_skills_id) && s.fk_position_id == positionSkilldto.positionId).ToListAsync();
            Console.WriteLine("hi - 4");
            var existingIds = existingSkills.Select(s => s.fk_skills_id);

            Console.WriteLine("hi - 5");
            foreach (var skill in existingSkills)
            {
                bool is_exist = positionSkilldto.minimumSkill.Contains(skill.fk_skills_id);
                if (is_exist)
                {
                    skill.is_min_req_skills = true;
                }
                else if (positionSkilldto.preferedSkill.Contains(skill.fk_skills_id))
                {
                    skill.is_min_req_skills = false;
                }
            }
            Console.WriteLine("hi - 6");
            var newMinSkills = positionSkilldto.minimumSkill.Where(id => !existingIds.Contains(id)).Select(id => new PositionSkillMapModel { fk_position_id = positionSkilldto.positionId, fk_skills_id = id, is_min_req_skills = true });
            var preferedSkills = positionSkilldto.preferedSkill.Where(id => !existingIds.Contains(id)).Select(id => new PositionSkillMapModel { fk_position_id = positionSkilldto.positionId, fk_skills_id = id, is_min_req_skills = false });

            await _context.PositionSkillMap.AddRangeAsync(newMinSkills);
            await _context.PositionSkillMap.AddRangeAsync(preferedSkills);

            await _context.PositionSkillMap.Where(s => !allIds.Contains(s.fk_skills_id) && s.fk_position_id == positionSkilldto.positionId).ExecuteDeleteAsync();

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            Console.WriteLine("hi - 7");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await transaction.RollbackAsync();
            return false;
        }
    }


    public async Task<List<Skilldto>> getAllPositionSkills(int positionId)
    {
        try
        {
            var result = from position in _context.Position join positionSkill in _context.PositionSkillMap on position.pk_position_id equals positionSkill.fk_position_id join skill in _context.Skills on positionSkill.fk_skills_id equals skill.pk_skills_id where position.pk_position_id == positionId select new { positionSkill, skill };
            List<Skilldto> positionSkills = new List<Skilldto>();
            foreach (var r in result)
            {
                Skilldto dto = new Skilldto();
                dto.pk_skills_id = r.skill.pk_skills_id;
                dto.skills_name = r.skill.skills_name;
                dto.skills_description = r.skill.skills_description;
                dto.is_min_req_skills = r.positionSkill.is_min_req_skills;
                positionSkills.Add(dto);
            }
            return positionSkills;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}