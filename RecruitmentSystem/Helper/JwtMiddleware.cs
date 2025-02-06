using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IEmployeeService employeeService, ICandidateService candidateService, string token)
        {
            try
            {
                Console.WriteLine("hi -1");
                var tokenHandler = new JwtSecurityTokenHandler();
                Console.WriteLine("hi -2");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                Console.WriteLine("hi -3");
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
                Console.WriteLine("hi -4");
                var jwtToken = (JwtSecurityToken)validateToken;
                int id = int.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "id").Value);
                bool is_candidate = bool.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "is_candidate").Value);
                context.Items["User"] = is_candidate ? candidateService.getCandidateByIdSync(id) : employeeService.getEmployeesByIdSync(id);
                Console.WriteLine("hi -1");
                context.Items["is_candidate"] = is_candidate;
            }
            catch (Exception ex)
            {
                Console.Write("exception in the jwt middleware");
                Console.WriteLine(ex.Message);
            }
        }
    }
}