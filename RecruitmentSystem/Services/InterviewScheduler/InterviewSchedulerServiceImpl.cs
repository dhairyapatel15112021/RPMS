using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.InterviewScheduler;

public class InterviewSchedulerServiceImpl : IInterviewSchedulerService
{
    private readonly ApplicationDbContext _context;

    public InterviewSchedulerServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<InterviewScheduledto> getScheduledInterview(int applicationId, bool isHr)
    {
        try
        {
            var interviewScheduledDto = await _context.InterviewSchedulers
                                        .Where(i => i.fk_application_id == applicationId)
                                        .Select(i => new InterviewScheduledto
                                        {
                                            interview = i,
                                            interview_rounds = i.CandidateInterview
                                            .Where(ir => ir.interview_type == (isHr ? InterviewType.hr : InterviewType.tech))
                                            .Select(ir => new CandidateInterviewModel
                                            {
                                                interview_date = ir.interview_date,
                                                interview_link = ir.interview_link,
                                                interview_time = ir.interview_time,
                                                interview_type = ir.interview_type,
                                                IsDone = ir.IsDone,
                                            })
                                            .ToList()
                                    })
                                    .FirstOrDefaultAsync();
                return interviewScheduledDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<InterviewSchedulerModel> getScheduledInterviewById(int interviewSchedulerId)
    {
        return await _context.InterviewSchedulers.FindAsync(interviewSchedulerId);
    }

    public async Task<bool> scheduleInterview(InterviewScheduledto interviewScheduledto)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.InterviewSchedulers.AddAsync(interviewScheduledto.interview);
            await _context.SaveChangesAsync();

            int scheduler_id = interviewScheduledto.interview.pk_interview_scheduler_id;
            foreach (CandidateInterviewModel c in interviewScheduledto.interview_rounds)
            {
                c.fk_interview_scheduler_id = scheduler_id;
            }
            await _context.Interviews.AddRangeAsync(interviewScheduledto.interview_rounds);
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
}