using RecruitmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.InterviewPanel;

public class InterviewPanelServiceImpl : IInterviewPanelService
{
    private readonly ApplicationDbContext _context;

    public InterviewPanelServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> assignInterviewer(List<int> ids,int positionId)
    {
       var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingInterviewer = await _context.InterviewPanels.Where(r => ids.Contains(r.fk_emp_interview_id) && r.fk_position_interview_id == positionId).ToListAsync();
            var existingIds = existingInterviewer.Select(r => r.fk_emp_interview_id);
            
            var newInterviewer = ids.Where(id => !existingIds.Contains(id)).Select(id => new InterviewPanelModel{fk_emp_interview_id = id,fk_position_interview_id = positionId});
            await _context.InterviewPanels.AddRangeAsync(newInterviewer);

            await _context.InterviewPanels.Where(r => !ids.Contains(r.fk_emp_interview_id) && r.fk_position_interview_id == positionId).ExecuteDeleteAsync();

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

    public async Task<InterviewPanelModel> getInterviwer(int empId, int positionId)
    {
        return await _context.InterviewPanels.FirstOrDefaultAsync(r => r.fk_emp_interview_id == empId && r.fk_position_interview_id == positionId);
    }

    public async Task<InterviewPanelModel> getInterviwerById(int interviwerId)
    {
        return await _context.InterviewPanels.FindAsync(interviwerId);
    }

    public async Task<List<EmployeeReviewerdto>> getInterviwerByPositionId(int positionId)
    {
        try
        {
            var result = from emp in _context.Employees join panel in _context.InterviewPanels on emp.pk_emp_id equals panel.fk_emp_interview_id where panel.fk_position_interview_id== positionId select new { emp };
            List<EmployeeReviewerdto> interviewers = new List<EmployeeReviewerdto>();
            foreach (var i in result)
            {
                EmployeeReviewerdto dto = new EmployeeReviewerdto();

                dto.emp_name = i.emp.emp_name;
                dto.pk_emp_id = i.emp.pk_emp_id;

                interviewers.Add(dto);
            }
            return interviewers;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<bool> removeInterviwer(int interviwerId)
    {
        try
        {
            InterviewPanelModel is_interviwer_panel = await getInterviwerById(interviwerId);
            if (is_interviwer_panel == null)
            {
                throw new Exception("Interviwer Not Exist");
            }
            await _context.InterviewPanels.Where(i => i.pk_interview_panel_id == interviwerId).ExecuteDeleteAsync();
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