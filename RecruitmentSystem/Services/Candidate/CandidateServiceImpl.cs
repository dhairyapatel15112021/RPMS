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

    public async Task<bool> addAllCandidates(List<Dictionary<string, string>> data)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            List<CandidateModel> allCandidates = new List<CandidateModel>();
            foreach (var dataEntry in data)
            {
                EmployeesModel is_employees_exist = await _context.Employees.FirstOrDefaultAsync(e => e.emp_email == dataEntry["email"]);
                CandidateModel is_candidate_exist = await getCandidateByEmail(dataEntry["email"]);
                if (is_employees_exist != null || is_candidate_exist != null)
                {
                    Console.WriteLine("Already Exist With This Email Id");
                    continue;
                }
                CandidateModel candidate = new CandidateModel();
                candidate.candidate_email = dataEntry["email"];
                candidate.candidate_contact_number = dataEntry["contact"];
                candidate.candidate_name = dataEntry["name"];
                candidate.candidate_password = BCrypt.Net.BCrypt.HashPassword(dataEntry["password"]);
                allCandidates.Add(candidate);
            }

            await _context.Candidate.AddRangeAsync(allCandidates);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
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
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            CandidateModel candidate = await getCandidateById(candidateId);
            if (candidate == null)
            {
                throw new Exception("Invalid Id");
            }
            candidate.cv_path = filePath;

            await _context.Applications.Where(app => app.fk_candidate_id == candidateId).ExecuteUpdateAsync(id => id.SetProperty(a => a.applicationStatus, ApplicationStatus.review));
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}