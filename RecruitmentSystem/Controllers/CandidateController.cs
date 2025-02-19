
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Candidate;
using RecruitmentSystem.Services.Employees;
using RecruitmentSystem.Services.Excel;

namespace RecruitmentSystem.Controllers;

// put,patch method to update candidate profile
// get method to see particluar candiate profile
[ApiController]
[Route("/api/candidate")]
public class CandidateController : ControllerBase
{

    public readonly IEmployeeService employeeService;

    public readonly ICandidateService candidateService;

    public readonly IExcelService excelService;

    public readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resumes");

    public CandidateController(IEmployeeService employeeService, ICandidateService candidateService, IExcelService excelService)
    {
        this.employeeService = employeeService;
        this.candidateService = candidateService;
        this.excelService = excelService;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    [HttpPost("add")]
    [Authorize(Roles = "admin,recruiter")]
    public async Task<ActionResult> addCandidate([FromBody] CandidateModel candidate)
    {
        try
        {
            if (candidate.candidate_email.Trim() == "" || candidate.candidate_contact_number == "" || candidate.candidate_contact_number.Trim().Length != 10 || candidate.candidate_name.Trim() == "" || candidate.candidate_password.Trim() == "")
            {
                throw new Exception("Please Enter Valid Data");
            }
            ;
            EmployeesModel is_employees_exist = await employeeService.getEmployeeByEmail(candidate.candidate_email);
            CandidateModel is_candidate_exist = await candidateService.getCandidateByEmail(candidate.candidate_email);
            if (is_employees_exist != null || is_candidate_exist != null)
            {
                throw new Exception("Already User Exist With This Email");
            }
            Boolean is_saved = await candidateService.addCandidate(candidate);
            if (is_saved)
            {
                return Ok("Candidate Added");
            }
            throw new Exception("Candidate Not Saved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add/all")]
    [Authorize(Roles = "admin,recruiter")]
    public async Task<ActionResult> addAllCandidate(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("File is empty");
            }
            List<Dictionary<string, string>> data = await excelService.extractData(file);
            bool is_all_saved = await candidateService.addAllCandidates(data);
            if (is_all_saved)
            {
                return Ok("Done");
            }
            return BadRequest("something went wrong");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get/all")]
    [Authorize(Roles = "admin,recruiter")]
    public async Task<ActionResult<List<CandidateModel>>> getAllCandidates()
    {
        try
        {
            List<CandidateModel> candidates = await candidateService.getAllCandidates();
            return Ok(candidates);
        }
        catch (Exception ex)
        {
            return BadRequest(null);
        }
    }

    [HttpPost("upload/cv/{candidateId}")]
    public async Task<IActionResult> uploadCV(IFormFile cv, int candidateId)
    {
        try
        {
            if (cv == null || cv.Length == 0)
            {
                return BadRequest("No File Uploaded");
            }

            var fileName = Path.GetFileNameWithoutExtension(cv.FileName) + "_" + candidateId + Path.GetExtension(cv.FileName);
            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cv.CopyToAsync(stream);
            }
            await candidateService.storeCvPathToDatabase($"/Resumes/{fileName}", candidateId);
            return Ok("Uploaded Sucessfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}