using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.InterviewScheduler;

public class InterviewSchedulerServiceImpl : IInterviewSchedulerService
{
    private readonly ApplicationDbContext _context;

    public InterviewSchedulerServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<InterviewSchedulerModel> getScheduledInterview(int applicationId)
    {
        return await _context.InterviewSchedulers.FirstOrDefaultAsync(i => i.fk_application_id == applicationId);
    }

    public async Task<InterviewSchedulerModel> getScheduledInterviewById(int interviewSchedulerId)
    {
        return await _context.InterviewSchedulers.FindAsync(interviewSchedulerId);
    }

    public async Task<InterviewSchedulerModel> scheduleInterview(InterviewSchedulerModel interviewScheduler)
    {
        try
        {
            InterviewSchedulerModel is_interview_scheduled = await getScheduledInterview(interviewScheduler.fk_application_id);
            if (is_interview_scheduled != null)
            {
                return is_interview_scheduled;
            }
            await _context.InterviewSchedulers.AddAsync(interviewScheduler);
            await _context.SaveChangesAsync();
            return interviewScheduler;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

    }
}