using Microsoft.AspNetCore.JsonPatch;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Application;

public interface IApplicationService
{
    Task<ApplicationModel> getApplication(int candidateId, int positionId);

    Task<bool> applyApplication(List<int> ids,int positionId);

    Task<List<ApplicationModel>> getAllApplicationOfCandidate(int candidateId);

    Task<bool> removeApplication(int applicationId);

    Task<bool> updateApplication(int applicationId, ApplicationModel application);

    Task<bool> applicationOnHold(int applicationId);

    Task<ApplicationModel> getApplicationById(int applicationId);
    Task<bool> changeApplication(int applicationId, JsonPatchDocument<ApplicationModel> application);

    // get all applicatin for particluar position
    Task<List<ApplicationModel>> getAllApplicationByPositionId(int positionId);
    Task<List<ApplicationPositiondto>> getAllByCandidateNotAppliedPosition(int positionId);
}