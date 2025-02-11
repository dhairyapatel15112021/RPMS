using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.ReviewPanel;

public class ReviewPanelServiceImpl : IReviewPanelSerivce
{
    private readonly ApplicationDbContext _context;

    public ReviewPanelServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> assignReviewer(ReviewerPanelModel reviewerPanel)
    {
        try
        {
            ReviewerPanelModel is_reviewer_exist = await getReviewer(reviewerPanel.fk_emp_review_id, reviewerPanel.fk_position_review_id);
            if (is_reviewer_exist != null)
            {
                throw new Exception("Reviewer Already Exist");
            }
            await _context.ReviewerPanels.AddAsync(reviewerPanel);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<ReviewerPanelModel> getReviewer(int empId, int positionId)
    {
        return await _context.ReviewerPanels.FirstOrDefaultAsync(r => r.fk_emp_review_id == empId && r.fk_position_review_id == positionId);
    }

    public async Task<ReviewerPanelModel> getReviewerById(int reviwerId)
    {
        return await _context.ReviewerPanels.FindAsync(reviwerId);
    }

    public async Task<List<ReviewerPanelModel>> getReviwerByPositionId(int positionId)
    {
        return await _context.ReviewerPanels.Where(r => r.fk_position_review_id == positionId).ToListAsync();
    }

    public async Task<bool> removeReviewer(int reviwerId)
    {
        try
        {
            ReviewerPanelModel is_reviewer_panel = await getReviewerById(reviwerId);
            if (is_reviewer_panel == null)
            {
                throw new Exception("Reviewer Not Exist");
            }
            await _context.ReviewerPanels.Where(r => r.pk_reviwer_panel_id == reviwerId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }


}