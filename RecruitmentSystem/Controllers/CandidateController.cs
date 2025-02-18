
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
            foreach (var dataEntry in data)
            {
                EmployeesModel is_employees_exist = await employeeService.getEmployeeByEmail(dataEntry["email"]);
                CandidateModel is_candidate_exist = await candidateService.getCandidateByEmail(dataEntry["email"]);
                if (is_employees_exist != null || is_candidate_exist != null)
                {
                    Console.WriteLine("Already Exist With This Email Id");
                    continue;
                }
                CandidateModel candidate = new CandidateModel();
                candidate.candidate_email = dataEntry["email"];
                candidate.candidate_contact_number = dataEntry["contact"];
                candidate.candidate_name = dataEntry["name"];
                candidate.candidate_password = dataEntry["password"];
                bool is_saved = await candidateService.addCandidate(candidate);
                if (is_saved)
                {
                    Console.WriteLine("Candidate with email " + candidate.candidate_email + " is saved");
                }
                else
                {
                    Console.WriteLine("Candidate with email " + candidate.candidate_email + " is not saved");
                }
            }
            return Ok(data);
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
            Console.WriteLine("HI -2 ");

            var fileName = Path.GetFileNameWithoutExtension(cv.FileName) + "_" + candidateId + Path.GetExtension(cv.FileName);
            var filePath = Path.Combine(folderPath, fileName);
            Console.WriteLine("HI -2 ");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cv.CopyToAsync(stream);
            }
            Console.WriteLine("HI -2 ");
            await candidateService.storeCvPathToDatabase($"/Resumes/{fileName}", candidateId);
            return Ok("Uploaded Sucessfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}