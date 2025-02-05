

using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Employees;

public interface IEmployeeService{

    Boolean addEmployees(EmployeesModel employees);
    Task<int> checkEmployeesCredentials(string email, string password);
    Task<List<EmployeesModel>> getAllEmployees();
    Task<EmployeesModel> getEmployeeByEmail(string email);
    Task<EmployeesModel> getEmployeesById(int id);

    EmployeesModel getEmployeesByIdSync(int id);
}