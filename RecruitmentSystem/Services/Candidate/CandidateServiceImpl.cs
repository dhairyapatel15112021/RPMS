using BCrypt.Net;
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

    public async Task<bool> addCandidate(CandidateModel candidate)
    {
        try
        {
            candidate.candidate_password = BCrypt.Net.BCrypt.HashPassword(candidate.candidate_password);
            await _context.Candidate.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<int> checkCandidateCredentials(string email, string password)
    {
        try
        {
            CandidateModel candidate = await getCandidateByEmail(email) ?? throw new Exception("Candidate Not Found");
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

    public async Task<List<CandidateModel>> getAllCandidates()
    {
        return await _context.Candidate.ToListAsync();
    }

    public async Task<CandidateModel> getCandidateByEmail(string email)
    {
        return await _context.Candidate.FirstOrDefaultAsync(c => c.candidate_email == email);
    }

    public async Task<CandidateModel> getCandidateById(int id)
    {
        return await _context.Candidate.FirstOrDefaultAsync(c => c.pk_candidate_id == id);
    }

    public CandidateModel getCandidateByIdSync(int id)
    {
        return _context.Candidate.FirstOrDefault(c => c.pk_candidate_id == id);
    }

    public async Task<bool> storeCvPathToDatabase(string filePath, int candidateId)
    {
        try
        {
            CandidateModel candidate = await getCandidateById(candidateId);
            if (candidate == null)
            {
                throw new Exception("Invalid Id");
            }
            candidate.cv_path = filePath;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}