using RecruitmentSystem.dto;

namespace RecruitmentSystem.Services.JWT;

public interface IJwtService{

    Task<Dictionary<string,List<string>>> generateToken(string email,bool is_candidate,int id,IConfiguration configuration);
    TokenValidator validateToken(string token);
}