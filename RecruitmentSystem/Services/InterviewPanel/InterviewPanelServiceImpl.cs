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

    public async Task<bool> assignInterviewer(InterviewPanelModel interviewPanel)
    {
        try
        {
            InterviewPanelModel is_interviewer_exist = await getInterviwer(interviewPanel.fk_emp_interview_id, interviewPanel.fk_position_interview_id);
            if (is_interviewer_exist != null)
            {
                throw new Exception("Interviwer Already Exist");
            }
            await _context.InterviewPanels.AddAsync(interviewPanel);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
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

    public async Task<List<InterviewPanelModel>> getInterviwerByPositionId(int positionId)
    {
        return await _context.InterviewPanels.Where(r => r.fk_position_interview_id == positionId).ToListAsync();

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