using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.dto;
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

    public PositionController(IPositionService positionService, IPositionSkillService positionSkillService)
    {
        this.positionService = positionService;
        this.positionSkillService = positionSkillService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> createOpening([FromBody] PositionModel position)
    {
        try
        {
            // validatation left
            Boolean is_saved = await positionService.createOpening(position);
            if (is_saved)
            {
                return Ok("Position Created");
            }
            throw new Exception("Position Is not saved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("hold/{positionId}")]
    public async Task<ActionResult> holdOpening(int positionId, [FromBody] JsonPatchDocument<HoldPostion> holdPosition)
    {
        try
        {
            if (positionId == 0 || holdPosition == null)
            {
                throw new Exception("Please Enter Valid Data");
            }

            bool is_updated = await positionService.holdOpening(positionId, holdPosition);
            if (!is_updated)
            {
                throw new Exception("No Updated");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPatch("close/{positionId}")]
    public async Task<ActionResult> closeOpening(int positionId, [FromBody] JsonPatchDocument<ClosePostion> closePosition)
    {
        try
        {
            if (positionId == 0 || closePosition == null)
            {
                throw new Exception("Please Enter Valid Data");
            }

            bool is_updated = await positionService.closeOpening(positionId, closePosition);
            if (!is_updated)
            {
                throw new Exception("No Updated");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("open/{positionId}")]
    public async Task<ActionResult> openOpening(int positionId)
    {
        try
        {
            if (positionId == 0)
            {
                throw new Exception("Please Enter Valid Data");
            }
            bool is_updated = await positionService.openOpening(positionId);
            if (!is_updated)
            {
                throw new Exception("No Updated");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{positionId}")]
    public async Task<ActionResult> updateOpening(int positionId, [FromBody] PositionModel position)
    {
        try
        {
            if (positionId != position.pk_position_id || position == null)
            {
                throw new Exception("Position Should be same or position should not be null");
            }
            bool is_updated = await positionService.updateOpening(positionId, position);
            if (!is_updated)
            {
                throw new Exception("No Updated");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("skill/add")]
    public async Task<ActionResult> addSkillToPosition([FromBody] PositionSkillMapModel positionSkillMapModel)
    {
        try
        {
            // validation left
            bool is_saved = await positionSkillService.addSkillToPosition(positionSkillMapModel);
            if (is_saved)
            {
                return Ok("Added");
            }
            throw new Exception("Not Saved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("skill/add/{positionId}")]
    public async Task<ActionResult> removeSkillToPosition(int positionId, [FromQuery] int skillId)
    {
        try
        {
            if (positionId == 0 || skillId == 0)
            {
                throw new Exception("Please Enter Valid Data");
            }
            bool is_saved = await positionSkillService.removeSkillToPosition(positionId, skillId);
            if (is_saved)
            {
                return Ok("Removed Succesfully");
            }
            throw new Exception("Not Removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get/all")]
    public async Task<ActionResult<PositionModel>> getAllOpening()
    {
        try
        {
            List<PositionModel> positions = await positionService.getAllOpenings();
            return Ok(positions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest();
        }
    }

    [HttpGet("get/open")]
    public async Task<ActionResult<PositionModel>> getAllOpenOpening()
    {
        try
        {
            List<PositionModel> positions = await positionService.getAllOpenOpenings();
            return Ok(positions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest();
        }
    }

}