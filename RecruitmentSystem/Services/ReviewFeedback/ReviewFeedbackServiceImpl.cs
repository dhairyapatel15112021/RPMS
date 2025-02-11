using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.ReviewFeedback;

public class ReviewFeedbackServiceImpl : IReviewFeebackService
{
    private readonly ApplicationDbContext _context;

    public ReviewFeedbackServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> addFeedback(ReviewFeedbackModel reviewFeedback)
    {
        try
        {
            ReviewFeedbackModel is_review_feedback_exist = await getFeedback(reviewFeedback.fk_application_id, reviewFeedback.fk_emp_id);
            if (is_review_feedback_exist != null)
            {
                is_review_feedback_exist.comments = reviewFeedback.comments;
                await _context.SaveChangesAsync();
                return "feedback Modified";
            }
            else
            {
                await _context.ReviewFeedbacks.AddAsync(reviewFeedback);
                await _context.SaveChangesAsync();
                return "feedback added";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<ReviewFeedbackModel> getFeedback(int applicationId, int empId)
    {
        return await _context.ReviewFeedbacks.FirstOrDefaultAsync(rf => rf.fk_application_id == applicationId && rf.fk_emp_id == empId);
    }
}