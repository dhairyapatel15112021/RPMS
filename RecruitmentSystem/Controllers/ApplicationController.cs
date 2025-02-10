using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Application;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/application")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        this.applicationService = applicationService;
    }

    [HttpPost("apply")]
    public async Task<ActionResult> apply([FromBody] ApplicationModel application)
    {
        try
        {
            if (application == null || application.fk_candidate_id == 0 || application.fk_position_id == 0)
            {
                return BadRequest("Please Enter Valid Data");
            }
            bool is_applied = await applicationService.applyApplication(application);
            if (!is_applied)
            {
                return BadRequest("Application Failed");
            }
            return Ok("Applied Succesfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get/{candidateId}")]
    public async Task<ActionResult<List<ApplicationModel>>> getAllApplications(int candidateId)
    {
        try
        {
            if (candidateId == 0)
            {
                return BadRequest("Please Enter Candidate id");
            }
            List<ApplicationModel> applications = await applicationService.getAllApplicationOfCandidate(candidateId);
            return Ok(applications);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remove/{applicationId}")]
    public async Task<ActionResult> removeApplication(int applicationId)
    {
        try
        {
            if (applicationId == 0)
            {
                return BadRequest("Please Enter Valid ApplicationId");
            }
            bool is_removed = await applicationService.removeApplication(applicationId);
            if (!is_removed)
            {
                return NotFound("Not Removed Please Try again");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{applicationId}")]
    public async Task<ActionResult> updateApplication(int applicationId, [FromBody] ApplicationModel application)
    {
        try
        {
            if (applicationId != application.pk_application_id)
            {
                return BadRequest("Both Id's Should be same");
            }
            bool is_updated = await applicationService.updateApplication(applicationId, application);
            if (!is_updated)
            {
                return BadRequest("Not Saved Please try again");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("hold/{applicationId}")]
    public async Task<ActionResult> applicationOnHold(int applicationId)
    {
        try
        {
            if (applicationId == 0)
            {
                return BadRequest("Please Enter ApplicationId");
            }
            bool is_hold = await applicationService.applicationOnHold(applicationId);
            if (!is_hold)
            {
                return BadRequest("No Chnage, Please Try again");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("change/{applicationId}")]
    public async Task<ActionResult> changeApplicationField(int applicationId, JsonPatchDocument<ApplicationModel> application)
    {
        try
        {
            if (application == null)
            {
                return BadRequest("Request Body Should not be null");
            }
            bool is_changed = await applicationService.changeApplication(applicationId, application);
            if (!is_changed)
            {
                return BadRequest("No Chnaged Please Try Again");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("position/get/all/{positionId}")]
    public async Task<ActionResult<List<ApplicationModel>>> getAllApplicationByPositionId(int positionId)
    {
        try
        {
            if(positionId == 0){
                return BadRequest("PositionId should not be empty");
            }
            List<ApplicationModel> applications = await applicationService.getAllApplicationByPositionId(positionId);
            if(applications == null){
                return NoContent();
            }
            return Ok(applications);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}