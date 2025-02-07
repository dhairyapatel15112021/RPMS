using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Candidate;
using RecruitmentSystem.Services.Employees;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("/api/employee")]
public class EmployeeController : ControllerBase
{

    public IEmployeeService employeeService;
    public ICandidateService candidateService;

    public EmployeeController(IEmployeeService employeeService, ICandidateService candidateService)
    {
        this.employeeService = employeeService;
        this.candidateService = candidateService;
    }

    [HttpPost]
    public async Task<ActionResult> addEmployees([FromBody] EmployeesModel employees)
    {
        try
        {
            if (employees.emp_contact_number == "" || employees.emp_contact_number.Trim().Length != 10 || employees.emp_name.Trim() == "" || employees.emp_email.Trim() == "" || employees.emp_designation.Trim() == "" || employees.emp_joining_date.ToString() == "")
            {
                throw new Exception("Please Enter Valid Data");
            }
            EmployeesModel is_employees_exist = await employeeService.getEmployeeByEmail(employees.emp_email);
            CandidateModel is_candidate_exist = await candidateService.getCandidateByEmail(employees.emp_email);
            if (is_employees_exist != null || is_candidate_exist != null)
            {
                throw new Exception("Already User Exist With This Email");
            }
            Boolean is_saved = await employeeService.addEmployees(employees);
            if (is_saved)
            {
                return Ok("Employee Added");
            }
            throw new Exception("Employee Not Saved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("get/all")]
    public async Task<ActionResult<List<EmployeesModel>>> getAllEmployees()
    {
        try
        {
            // changes needed
            List<EmployeesModel> employees = await employeeService.getAllEmployees();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<EmployeesModel>> getAllEmployee(int id)
    {
        try
        {
            // changes needed
            EmployeesModel employees = await employeeService.getEmployeesById(id);
            return Ok(employees);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

}