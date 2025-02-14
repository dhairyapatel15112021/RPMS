using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.InterviewPanel;
using RecruitmentSystem.Services.Position;
using RecruitmentSystem.Services.PositionSkill;
using RecruitmentSystem.Services.ReviewPanel;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/position")]
public class PositionController : ControllerBase
{
    private readonly IPositionService positionService;
    private readonly IPositionSkillService positionSkillService;
    private readonly IReviewPanelSerivce reviewPanelSerivce;
    private readonly IInterviewPanelService interviewPanelService;

    public PositionController(IPositionService positionService, IPositionSkillService positionSkillService, IReviewPanelSerivce reviewPanelSerivce, IInterviewPanelService interviewPanelService)
    {
        this.positionService = positionService;
        this.positionSkillService = positionSkillService;
        this.reviewPanelSerivce = reviewPanelSerivce;
        this.interviewPanelService = interviewPanelService;
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
    [Authorize(Roles ="admin,recruiter")]
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
    [Authorize(Roles ="admin,recruiter")]
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
    [Authorize(Roles ="admin,recruiter")]
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

    [HttpGet("skill/get/all")]
    public async Task<ActionResult> getAllPositionSkills(){
        try{
            positionSkillService.getAllPositionSkills();
            return Ok();
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("skill/remove/{positionId}")]
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
    [Authorize(Roles ="admin,recruiter")]
    public async Task<ActionResult<PositionDTO>> getAllOpening()
    {
        try
        {
            List<PositionDTO> positions = await positionService.getAllOpenings();
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

    [HttpPost("reviewer/add")]
    public async Task<ActionResult> assignReviewer([FromBody] ReviewerPanelModel reviewerPanel)
    {
        try
        {
            if (reviewerPanel.fk_emp_review_id == 0 || reviewerPanel.fk_position_review_id == 0 || reviewerPanel.review_deadline.ToString().Trim() == "")
            {
                return BadRequest("Please Enter Valid Data");
            }
            bool is_assigned = await reviewPanelSerivce.assignReviewer(reviewerPanel);
            if (!is_assigned)
            {
                return BadRequest("Not Saved");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("reviewer/remove/{reviewId}")]
    public async Task<ActionResult> removeReviwer(int reviewId)
    {
        try
        {
            if (reviewId == 0)
            {
                return BadRequest("Please Enter Valid Data");
            }
            bool is_removed = await reviewPanelSerivce.removeReviewer(reviewId);
            if (!is_removed)
            {
                return BadRequest("Not Removed");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("reviewer/get")]
    public async Task<ActionResult<List<ReviewerPanelModel>>> getReviewrPanel([FromQuery] int positionId)
    {
        try
        {
            if (positionId == 0)
            {
                throw new Exception("Please Enter PositionId");
            }
            List<ReviewerPanelModel> reviewer = await reviewPanelSerivce.getReviwerByPositionId(positionId);
            return Ok(reviewer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(null);
        }
    }

    [HttpPost("interviwer/add")]
    public async Task<ActionResult> assignInterviewer([FromBody] InterviewPanelModel interviewPanel)
    {
        try
        {
            if (interviewPanel.fk_emp_interview_id == 0 || interviewPanel.fk_position_interview_id == 0)
            {
                return BadRequest("Please Enter Valid Data");
            }
            bool is_assigned = await interviewPanelService.assignInterviewer(interviewPanel);
            if (!is_assigned)
            {
                return BadRequest("Not Saved");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("interviwer/remove/{interviwerId}")]
    public async Task<ActionResult> removeInterviwer(int interviwerId)
    {
        try
        {
            if (interviwerId == 0)
            {
                return BadRequest("Please Enter Valid Data");
            }
            bool is_removed = await interviewPanelService.removeInterviwer(interviwerId);
            if (!is_removed)
            {
                return BadRequest("Not Removed");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("interviewer/get")]
    public async Task<ActionResult<List<InterviewPanelModel>>> getInterviewerPanel([FromQuery] int positionId)
    {
        try
        {
            if (positionId == 0)
            {
                throw new Exception("Please Enter PositionId");
            }
            List<InterviewPanelModel> interviwer = await interviewPanelService.getInterviwerByPositionId(positionId);
            return Ok(interviwer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(null);
        }
    }
}