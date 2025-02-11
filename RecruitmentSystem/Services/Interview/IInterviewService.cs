using Microsoft.AspNetCore.JsonPatch;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Interview;

public interface IInterviewService{
    Task<bool> registerInterview(CandidateInterviewModel interview);
    Task<CandidateInterviewModel> getInterview(int interviewSchedulerId,int roundNumber);

    Task<bool> validateInterviewRound(CandidateInterviewModel interviewModel);

    Task<CandidateInterviewModel> getInterviewById(int interviewId);

    Task<bool> addRemakrs(int interviewId,JsonPatchDocument<CandidateInterviewModel> interviewPatch);
}