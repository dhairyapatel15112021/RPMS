namespace RecruitmentSystem.Services.Employees;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

public class EmployeeServiceImpl : IEmployeeService
{
  private readonly ApplicationDbContext _context;

  public EmployeeServiceImpl(ApplicationDbContext context)
  {
    _context = context;
  }

  public Boolean addEmployees(EmployeesModel employees)
  {
    try
    {
      employees.emp_password = BCrypt.Net.BCrypt.HashPassword(employees.emp_password);
      _context.Employees.Add(employees);
      _context.SaveChanges();
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return false;
    }
  }

  public async Task<int> checkEmployeesCredentials(string email, string password)
  {
    try
    {
      EmployeesModel candidate = await getEmployeeByEmail(email) ?? throw new Exception("Employee Not Found");
      bool verified = BCrypt.Net.BCrypt.Verify(password, candidate.emp_password);
      if (!verified)
      {
        throw new Exception("Incorrect Password");
      }
      return candidate.pk_emp_id;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      return -1;
    }
  }

  public async Task<List<EmployeesModel>> getAllEmployees()
  {
    return await _context.Employees.ToListAsync();
  }

  public async Task<EmployeesModel> getEmployeeByEmail(string email)
  {
    return await _context.Employees.FirstOrDefaultAsync(e => e.emp_email == email);
  }

  public async Task<EmployeesModel> getEmployeesById(int id)
  {
    return await _context.Employees.FirstOrDefaultAsync(e => e.pk_emp_id == id);
  }

    public EmployeesModel getEmployeesByIdSync(int id)
    {
          return _context.Employees.FirstOrDefault(e => e.pk_emp_id == id);
    }
}