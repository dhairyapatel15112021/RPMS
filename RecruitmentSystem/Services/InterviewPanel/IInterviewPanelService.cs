using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.InterviewPanel;

public interface IInterviewPanelService
{
    Task<bool> assignInterviewer(InterviewPanelModel interviewPanel);

    Task<InterviewPanelModel> getInterviwer(int empId, int positionId);

    Task<bool> removeInterviwer(int interviwerId);

    Task<InterviewPanelModel> getInterviwerById(int interviwerId);

    Task<List<InterviewPanelModel>> getInterviwerByPositionId(int positionId);
}