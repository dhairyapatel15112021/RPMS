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

    [HttpPost]
    public async Task<ActionResult> addSkills([FromBody] SkillsModel skills)
    {
        try
        {
            if (skills.skills_name.Trim() == "" || skills.skills_description.Trim() == "")
            {
                throw new Exception("Please Enter Valid Data of skills");
            }
            Boolean is_saved = skillsService.createSkill(skills);
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
}