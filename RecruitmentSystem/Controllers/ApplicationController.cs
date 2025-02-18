using DocumentFormat.OpenXml.Office2021.PowerPoint.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Application;
using RecruitmentSystem.Services.Email;
using RecruitmentSystem.Services.InterviewFeedback;
using RecruitmentSystem.Services.ReviewFeedback;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/application")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService applicationService;
    private readonly IInterviewFeedbackService interviewFeedbackService;
    private readonly IReviewFeebackService reviewFeebackService;
    private readonly IEmailService emailService;

    public ApplicationController(IApplicationService applicationService, IReviewFeebackService reviewFeebackService, IInterviewFeedbackService interviewFeedbackService, IEmailService emailService)
    {
        this.applicationService = applicationService;
        this.interviewFeedbackService = interviewFeedbackService;
        this.reviewFeebackService = reviewFeebackService;
        this.emailService = emailService;
    }

    [HttpPost("apply/{positionId}")]
    [Authorize(Roles = "admin,recruiter")]
    public async Task<ActionResult> apply([FromBody] List<int> ids, int positionId)
    {
        try
        {
            if (ids == null || ids.Count == 0)
            {
                throw new Exception("Please Enter Proper Data");
            }
            bool is_applied = await applicationService.applyApplication(ids, positionId);
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

    [HttpGet("candidate/get/all/{candidateId}")]
    [Authorize(Roles = "recruiter,admin,candidate")]
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
    [Authorize("recruiter,admin")]
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
    [Authorize("admin,recruiter")]
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
    [Authorize(Roles = "hr,recruiter,admin")]
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
            bool is_send = await emailService.sendMails(["pateldhairya0210@gmail.com", "parthpatel06072004@gmail.com"], "new email", "<h1>Hello, Your Application Is on hold</h1>");
            if (is_send)
            {
                return Ok("send email");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("change/{applicationId}")]
    [Authorize(Roles = "Reviewer,admin")]
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
    [Authorize(Roles = "recruiter,admin,Reviewer,Interviewer")]
    public async Task<ActionResult<List<Applicationdto>>> getAllApplicationByPositionId([FromQuery] int id, int positionId)
    {
        // here 1 means recruiter or admin
        // here 2 reviewer
        // here 3 interviewewr
        // here 4 hr
        try
        {
            if (positionId == 0)
            {
                return BadRequest("PositionId should not be empty");
            }
            List<Applicationdto> applications = await applicationService.getAllApplicationByPositionId(id, positionId);
            if (applications == null)
            {
                return NoContent();
            }
            return Ok(applications);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("review/feedback/add")]
    [Authorize(Roles = "Reviewer,admin")]
    public async Task<ActionResult> addReviewFeedback([FromBody] ReviewFeedbackModel reviewFeedback)
    {
        try
        {
            if (reviewFeedback == null || reviewFeedback.comments.Trim() == "" || reviewFeedback.fk_emp_id == 0 || reviewFeedback.fk_application_id == 0)
            {
                BadRequest("Please Enter Valid Data");
            }
            string feedback = await reviewFeebackService.addFeedback(reviewFeedback);
            if (feedback == null)
                throw new Exception("Something Went Wrong");
            return Ok(feedback);
        }
        catch (Exception ex)
        {
            Console.WriteLine("2");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get/all/{positionId}")]
    [Authorize(Roles = "admin,recruiter")]
    public async Task<ActionResult> getAllCandidatesByPositionId(int positionId)
    {
        try
        {
            List<ApplicationPositiondto> candidates = await applicationService.getAllByCandidateNotAppliedPosition(positionId);
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet("get/review/{employeeId}")]
    [Authorize(Roles = "Reviewer,admin")]
    public async Task<ActionResult> getRevieweOfApplication([FromQuery] int applicationId, int employeeId)
    {
        try
        {
            ReviewFeedbackModel reviewe = await reviewFeebackService.getFeedback(applicationId, employeeId);
            return Ok(reviewe.comments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}