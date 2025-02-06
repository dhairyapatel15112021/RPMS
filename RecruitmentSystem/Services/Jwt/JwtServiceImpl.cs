using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RecruitmentSystem.Services.RoleMap;
using System.Threading.Tasks;

namespace RecruitmentSystem.Services.JWT;

public class JwtServiceImpl : IJwtService
{
    private readonly IRoleMapService roleMapService;

    public JwtServiceImpl(IRoleMapService roleMapService)
    {
        this.roleMapService = roleMapService;
    }
    public async Task<string> generateToken(string email, bool is_candidate, int id, IConfiguration _configuration)
    {
        // List<Claim> claims = new List<Claim>() {
        //                 new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                 new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                 new Claim("email", email),
        //                 new Claim("is_candidate", is_candidate.ToString()),
        //                 new Claim("id", id.ToString())
        //             };
                List<Claim> claims = new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("email", email),
                        new Claim("is_candidate", is_candidate.ToString()),
                        new Claim("id", id.ToString())
                    };
       
        // get roles of id; 
        List<string> roles = await roleMapService.getRoles(id);
        foreach (var role in roles)
        {
            claims.Add(new Claim("Role", Convert.ToString(role)));
        }
        if (roles.Count == 0)
        {
            claims.Add(new Claim("Role", Convert.ToString("candidate")));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signIn);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}