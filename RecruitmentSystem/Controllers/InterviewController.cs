using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Interview;
using RecruitmentSystem.Services.InterviewScheduler;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/interview")]
public class InterviewController : ControllerBase
{
    private readonly IInterviewSchedulerService interviewSchedulerService;
    private readonly IInterviewService interviewService;

    public InterviewController(IInterviewSchedulerService interviewSchedulerService, IInterviewService interviewService)
    {
        this.interviewSchedulerService = interviewSchedulerService;
        this.interviewService = interviewService;
    }

    [HttpPost("schedule")]
    [Authorize(Roles = "admin,recruiter")]
    public async Task<ActionResult<InterviewSchedulerModel>> scheduleInterview([FromBody] InterviewScheduledto interviewdto)
    {
        try
        {
            // validation left
            bool scheduled_interview = await interviewSchedulerService.scheduleInterview(interviewdto);
            if (scheduled_interview == false)
            {
                throw new Exception("Something Went Wrong, Interview Not Scheduled");
            }
            return Ok(scheduled_interview);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("schedule/{applicationId}")]
    [Authorize(Roles = "admin,recruiter,Interviewer,hr")]
    public async Task<ActionResult<InterviewScheduledto>> getScheduleInterview(int applicationId, [FromQuery] bool isHr)
    {
        try
        {
            InterviewScheduledto interviewScheduler = await interviewSchedulerService.getScheduledInterview(applicationId, isHr);
            return Ok(interviewScheduler);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("round/register")]
    public async Task<ActionResult> interviewRoundRegister([FromBody] CandidateInterviewModel interview)
    {
        try
        {
            // validation left
            bool is_interview_round_register = await interviewService.registerInterview(interview);
            if (!is_interview_round_register)
            {
                return BadRequest("Something Went Wrong");
            }
            return Ok("registered");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("round/change/{interviewId}")]
    public async Task<ActionResult> interviewRoundChange(int interviewId, JsonPatchDocument<CandidateInterviewModel> interviewPatch)
    {
        try
        {
            // validation left
            bool is_interview_round_change = await interviewService.addRemakrs(interviewId, interviewPatch);
            if (!is_interview_round_change)
            {
                return BadRequest("Something Went Wrong");
            }
            return Ok("Changed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}