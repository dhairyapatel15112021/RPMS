using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.ReviewPanel;

public interface IReviewPanelSerivce
{
    Task<bool> assignReviewer(List<int> ids,int positionId);

    Task<ReviewerPanelModel> getReviewer(int empId, int positionId);

    Task<bool> removeReviewer(int reviwerId);

    Task<ReviewerPanelModel> getReviewerById(int reviwerId);

    Task<List<EmployeeReviewerdto>> getReviwerByPositionId(int positionId);
    List<PositionDTO> getPositionByEmployeeId(int employeeId);
}