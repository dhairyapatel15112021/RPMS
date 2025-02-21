using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.ReviewPanel;

public class ReviewPanelServiceImpl : IReviewPanelSerivce
{
    private readonly ApplicationDbContext _context;

    public ReviewPanelServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> assignReviewer(List<int> ids, int positionId)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingReviewer = await _context.ReviewerPanels.Where(r => ids.Contains(r.fk_emp_review_id) && r.fk_position_review_id == positionId).ToListAsync();
            var existingIds = existingReviewer.Select(r => r.fk_emp_review_id);

            var newReviewer = ids.Where(id => !existingIds.Contains(id)).Select(id => new ReviewerPanelModel { fk_emp_review_id = id, fk_position_review_id = positionId, review_deadline = DateTime.Now });
            await _context.ReviewerPanels.AddRangeAsync(newReviewer);

            await _context.ReviewerPanels.Where(r => !ids.Contains(r.fk_emp_review_id) && r.fk_position_review_id == positionId).ExecuteDeleteAsync();

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public List<PositionDTO> getPositionByEmployeeId(int employeeId)
    {
        try
        {
            var allPositions = from reviewe in _context.ReviewerPanels join pos in _context.Position on reviewe.fk_position_review_id equals pos.pk_position_id where reviewe.fk_emp_review_id == employeeId select new { pos };
            List<PositionDTO> positions = new List<PositionDTO>();

            foreach (var p in allPositions)
            {
                PositionDTO dto = new PositionDTO();
                dto.candidate_name = null;
                dto.emp_name = null;
                dto.pk_position_id = p.pos.pk_position_id;
                dto.position_title = p.pos.position_title;
                dto.position_description = p.pos.position_description;
                dto.position_min_experience = p.pos.position_min_experience;
                dto.is_open = p.pos.is_open;
                dto.comments = p.pos.comments;
                dto.position_level = p.pos.position_level;
                dto.position_location = p.pos.position_location;
                dto.position_creation_date = p.pos.position_creation_date;
                dto.salary_range = p.pos.salary_range;
                dto.qualification = p.pos.qualification;

                positions.Add(dto);
            }

            return positions;
        }
        catch (Exception ex)
        {
            return null;
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

    public async Task<List<EmployeeReviewerdto>> getReviwerByPositionId(int positionId)
    {
        try
        {
            var result = from emp in _context.Employees join panel in _context.ReviewerPanels on emp.pk_emp_id equals panel.fk_emp_review_id where panel.fk_position_review_id == positionId select new { emp };
            List<EmployeeReviewerdto> reviewers = new List<EmployeeReviewerdto>();
            foreach (var r in result)
            {
                EmployeeReviewerdto dto = new EmployeeReviewerdto();

                dto.emp_name = r.emp.emp_name;
                dto.pk_emp_id = r.emp.pk_emp_id;

                reviewers.Add(dto);
            }
            return reviewers;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }



}