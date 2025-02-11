using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.ReviewPanel;

public interface IReviewPanelSerivce
{
    Task<bool> assignReviewer(ReviewerPanelModel reviewerPanel);

    Task<ReviewerPanelModel> getReviewer(int empId, int positionId);

    Task<bool> removeReviewer(int reviwerId);

    Task<ReviewerPanelModel> getReviewerById(int reviwerId);

    Task<List<ReviewerPanelModel>> getReviwerByPositionId(int positionId);
}