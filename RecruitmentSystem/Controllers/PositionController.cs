using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Position;
using RecruitmentSystem.Services.PositionSkill;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/position")]
public class PositionController : ControllerBase
{
    private IPositionService positionService;
    private IPositionSkillService positionSkillService;

    public PositionController(IPositionService positionService,IPositionSkillService positionSkillService)
    {
        this.positionService = positionService;
        this.positionSkillService = positionSkillService;
    }

    [HttpPost]
    public async Task<ActionResult> createOpening([FromBody] PositionModel position)
    {
        try
        {
            // validatation left
            Boolean is_saved = positionService.createOpening(position);
            if(is_saved){
                return Ok("Position Created");
            }
            throw new Exception("Position Is not saved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("skill/add")]
    public async Task<ActionResult> addSkillToPosition([FromBody] PositionSkillMapModel positionSkillMapModel){
        try{
            // validation left
            bool is_saved = positionSkillService.addSkillToPosition(positionSkillMapModel);
            if(is_saved){
                  return Ok("Added");
            }
            throw new Exception("Not Saved");
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

}