using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RecruitmentSystem.Services.RoleMap;
using System.Threading.Tasks;
using RecruitmentSystem.dto;

namespace RecruitmentSystem.Services.JWT;

public class JwtServiceImpl : IJwtService
{
    private readonly IRoleMapService roleMapService;
    private readonly IConfiguration _configuration;

    public JwtServiceImpl(IRoleMapService roleMapService, IConfiguration configuration)
    {
        this.roleMapService = roleMapService;
        this._configuration = configuration;
    }
    public async Task<Dictionary<string, List<string>>> generateToken(string email, bool is_candidate, int id, IConfiguration _configuration)
    {
        Dictionary<string, List<string>> res = new Dictionary<string, List<string>>();
        List<Claim> claims = new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("email", email),
                        new Claim("is_candidate", is_candidate.ToString()),
                        new Claim("id", id.ToString())
                    };

        List<string> roles = await roleMapService.getRoles(id);
        if (is_candidate)
        {
            claims.Add(new Claim(ClaimTypes.Role, Convert.ToString("candidate")));
        }
        else
        {
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, Convert.ToString(role)));
            }
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signIn);

        res.Add("token", [new JwtSecurityTokenHandler().WriteToken(token)]);
        res.Add("roles", is_candidate ? ["candidate"] : roles);

        return res;
    }

    public TokenValidator validateToken(string token)
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
            List<string> roles = new List<string>((IEnumerable<string>)jwtToken.Claims.Where(e => e.Type == ClaimTypes.Role).Select(v => v.Value).ToList());

            TokenValidator result = new TokenValidator();
            result.id = id;
            result.is_candidate = is_candidate;
            result.roles = roles;
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}