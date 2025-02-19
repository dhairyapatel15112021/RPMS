using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Candidate;

public interface ICandidateService
{
    Task<CandidateModel> getCandidateByEmail(string email);

    Task<CandidateModel> getCandidateById(int id);

    CandidateModel getCandidateByIdSync(int id);
    Task<int> checkCandidateCredentials(string name, string password);

    Task<bool> addCandidate(CandidateModel candidate);
    Task<List<CandidateModel>> getAllCandidates();
    Task<bool> storeCvPathToDatabase(string filePath, int candidateId);
    Task<bool> addAllCandidates(List<Dictionary<string, string>> data);
}