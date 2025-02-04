namespace RecruitmentSystem.Services.JWT;

public interface IJwtService{

    string generateToken(string email,bool is_candidate,int id,IConfiguration configuration);

}