using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.InterviewPanel;

public interface IInterviewPanelService
{
    Task<bool> assignInterviewer(List<int> ids,int positionId);

    Task<InterviewPanelModel> getInterviwer(int empId, int positionId);

    Task<bool> removeInterviwer(int interviwerId);

    Task<InterviewPanelModel> getInterviwerById(int interviwerId);

    Task<List<EmployeeReviewerdto>> getInterviwerByPositionId(int positionId);
}