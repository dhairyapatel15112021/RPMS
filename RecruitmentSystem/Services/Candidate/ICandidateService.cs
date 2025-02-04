using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Candidate;

public interface ICandidateService
{
    Task<CandidateModel> getCandidate(string email);

    Task<int> checkCandidateCredentials(string name, string password);
}