using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.dto;
using RecruitmentSystem.Services.Candidate;
using RecruitmentSystem.Services.Employees;
using RecruitmentSystem.Services.JWT;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IEmployeeService employeeService;

    private readonly ICandidateService candidateService;

    private readonly IJwtService jwtService;

    private readonly IConfiguration configuration;

    public AuthController(IEmployeeService employeeService, ICandidateService candidateService, IJwtService jwtService, IConfiguration configuration)
    {
        this.employeeService = employeeService;
        this.candidateService = candidateService;
        this.jwtService = jwtService;
        this.configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> login([FromBody] login login)
    {
        try
        {
            int id = -1;
            if (login.is_candidate)
            {
                id = await candidateService.checkCandidateCredentials(login.email, login.password);
            }
            else
            {
                id = await employeeService.checkEmployeesCredentials(login.email, login.password);
            }
            if (id == -1)
            {
                throw new Exception("Data is Incorrect");
            }
            string token = await jwtService.generateToken(login.email, login.is_candidate, id, configuration);

            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}