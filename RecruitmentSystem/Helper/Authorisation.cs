using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.RoleMap;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RecruitmentSystem.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorization : Attribute , IAuthorizationFilter
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
            try{
            Console.WriteLine("inside on auth");
            var isRolePermission = false;
            bool is_candidate = (bool)context.HttpContext.Items["is_candidate"];

            CandidateModel candidate = null;
            EmployeesModel employees = null;
            Console.WriteLine("this.roles" + this._roles.ToString());
            if (is_candidate)
            {
                candidate = (CandidateModel)context.HttpContext.Items["User"];
            }
            else
            {
                employees = (EmployeesModel)context.HttpContext.Items["User"];
            }
            Console.WriteLine("On Auth files");
            if (candidate == null && employees == null)
            {
                context.Result = new JsonResult(new { Message = "Unauthorization" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (candidate != null)
            {
                foreach (var authRole in this._roles)
                {
                    if (authRole == "candidate")
                    {
                        isRolePermission = true;
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
                        if (roles[i] == this._roles[i])
                        {
                            isRolePermission = true;
                            break;
                        }
                    }
                }
            }

            if (!isRolePermission)
                context.Result = new JsonResult(new { Message = "Unauthorization" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
    }
}