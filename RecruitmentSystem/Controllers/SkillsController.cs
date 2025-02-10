using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Skills;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("/api/skills")]
public class SkillsController : ControllerBase
{

    private ISkillsService skillsService;

    public SkillsController(ISkillsService skillsService)
    {
        this.skillsService = skillsService;
    }

    [HttpPost("add")]
    public async Task<ActionResult> addSkills([FromBody] SkillsModel skills)
    {
        try
        {
            if (skills.skills_name.Trim() == "" || skills.skills_description.Trim() == "")
            {
                throw new Exception("Please Enter Valid Data of skills");
            }
            bool is_saved = await skillsService.createSkill(skills);
            if (is_saved)
            {
                return Ok("Skills Added");
            }
            throw new Exception("Skills Not Saved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> updateSkills(int id, [FromBody] SkillsModel skills)
    {
        try
        {
            if (id != skills.pk_skills_id)
            {
                throw new Exception("Id should be same");
            }
            if (skills.skills_name.Trim() == "" || skills.skills_description.Trim() == "")
            {
                throw new Exception("Please Enter Valid Data of skills");
            }
            bool is_updated = await skillsService.updateSkill(id,skills);
            if (is_updated)
            {
                return Ok("Updated Successfully");
            }
            throw new Exception("Skills Not Updated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}