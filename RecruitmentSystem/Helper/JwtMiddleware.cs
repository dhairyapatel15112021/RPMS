using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using RecruitmentSystem.Services.Candidate;
using RecruitmentSystem.Services.Employees;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.RoleMap;
using System.Threading.Tasks;


namespace RecruitmentSystem.Helper;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;



    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        this._next = next;
        this._configuration = configuration;
    }

    public async Task Invoke(HttpContext context, IEmployeeService employeeService, ICandidateService candidateService)
    {

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
            //Validate Token
            attachUserToContext(context, employeeService, candidateService, token);
        _next(context);
    }

    private async Task attachUserToContext(HttpContext context, IEmployeeService employeeService, ICandidateService candidateService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"]
            }, out SecurityToken validateToken);
            var jwtToken = (JwtSecurityToken)validateToken;
            int id = int.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "id").Value);
            bool is_candidate = bool.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "is_candidate").Value);
           context.Items["User"] = is_candidate ? candidateService.getCandidateByIdSync(id) : employeeService.getEmployeesByIdSync(id);

            context.Items["is_candidate"] = is_candidate;
        }
        catch (Exception ex)
        {
            Console.Write("exception in the jwt middleware");
            Console.WriteLine(ex.Message);
        }
    }
}


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class Authorization : Attribute, IAuthorizationFilter
{
    private readonly IList<string> _roles;
    private readonly IRoleMapService roleMapService;
    public Authorization(IRoleMapService roleMapService, params string[] _roles)
    {
        this._roles = _roles ?? new string[] { };
        this.roleMapService = roleMapService;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isRolePermission = true;
        bool is_candidate = (bool)context.HttpContext.Items["is_candidate"];

        CandidateModel candidate = null;
        EmployeesModel employees = null;
        if (is_candidate)
        {
            candidate = (CandidateModel)context.HttpContext.Items["User"];
        }
        else
        {
            employees = (EmployeesModel)context.HttpContext.Items["User"];
        }

        if (candidate == null && employees == null)
        {
            context.Result = new JsonResult(new { Message = "Unauthorization" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        if (candidate != null)
        {
            foreach (var authRole in this._roles)
            {
                if (authRole != "candidate")
                {
                    isRolePermission = false;
                }
            }
        }
        else if (employees != null && this._roles.Any())
        {
            // foreach (var userRole in employees.RoleMap)
            // {
            //     foreach (var AuthRole in this._roles)
            //     {

            //         if (userRole.Role.role_type == AuthRole)
            //         {
            //             isRolePermission = true;
            //         }
            //     }
            // }
            this._roles.OrderBy(c => c);
            List<string> roles = (List<string>)roleMapService.getRolesSync(employees.pk_emp_id).OrderBy(c => c);
           // List<string> roles = ["admin"];
            if (this._roles.Count != roles.Count)
            {
                isRolePermission = false;
            }
            else
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    if (roles[i] != this._roles[i])
                    {
                        isRolePermission = false;
                        break;
                    }
                }
            }
        }

        if (!isRolePermission)
            context.Result = new JsonResult(new { Message = "Unauthorization" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}