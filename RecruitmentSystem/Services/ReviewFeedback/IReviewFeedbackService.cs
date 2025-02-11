using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.ReviewFeedback;

public interface IReviewFeebackService{
    Task<ReviewFeedbackModel> getFeedback(int applicationId,int empId);

    Task<string> addFeedback(ReviewFeedbackModel reviewFeedback);

}