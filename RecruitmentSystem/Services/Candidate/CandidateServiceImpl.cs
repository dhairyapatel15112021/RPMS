using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Candidate;

public class CandidateServiceImpl : ICandidateService
{
    private readonly ApplicationDbContext _context;

    public CandidateServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> checkCandidateCredentials(string email, string password)
    {
        try
        {
            CandidateModel candidate = await getCandidate(email) ?? throw new Exception("Candidate Not Found");
            bool verified = BCrypt.Net.BCrypt.Verify(password, candidate.candidate_password);
            if (!verified)
            {
                throw new Exception("Incorrect Password");
            }
            return candidate.pk_candidate_id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }
    }
    public async Task<CandidateModel> getCandidate(string email)
    {
        return await _context.Candidate.FirstOrDefaultAsync(c => c.candidate_email == email);
    }
}