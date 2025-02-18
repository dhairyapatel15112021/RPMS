using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RecruitmentSystem.Services.Candidate;
using RecruitmentSystem.Services.Employees;

namespace RecruitmentSystem.Helper
{

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

            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                await attachUserToContext(context, employeeService, candidateService, token);

            Console.WriteLine(context.Items["User"]);
            Console.WriteLine(context.Items["is_candidate"]);
            await _next(context);
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
                var identity = new ClaimsIdentity(jwtToken.Claims, JwtBearerDefaults.AuthenticationScheme);
                context.User = new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                Console.Write("exception in the jwt middleware");
                Console.WriteLine(ex.Message);
            }
        }
    }
}