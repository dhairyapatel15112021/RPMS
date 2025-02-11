using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Interview;

public class InterviewServiceImpl : IInterviewService
{
    private readonly ApplicationDbContext _context;

    public InterviewServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> addRemakrs(int interviewId, JsonPatchDocument<CandidateInterviewModel> interviewPatch)
    {
        try
        {
            CandidateInterviewModel is_interview_exist = await getInterviewById(interviewId);
            if (is_interview_exist == null)
            {
                throw new Exception("No Interview Exists");
            }
            interviewPatch.ApplyTo(is_interview_exist);
            bool validate_interview = await validateInterviewRound(is_interview_exist);
            if (!validate_interview)
            {
                throw new Exception("Round number is negative or round count is exceeds compare to define interview round");
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<CandidateInterviewModel> getInterview(int interviewSchedulerId, int roundNumber)
    {
        return await _context.Interviews.FirstOrDefaultAsync(it => it.fk_interview_scheduler_id == interviewSchedulerId && it.round_number == roundNumber);
    }

    public async Task<CandidateInterviewModel> getInterviewById(int interviewId)
    {
        return await _context.Interviews.FindAsync(interviewId);
    }

    public async Task<bool> registerInterview(CandidateInterviewModel interview)
    {
        try
        {
            bool validate_interview = await validateInterviewRound(interview);
            if (!validate_interview)
            {
                throw new Exception("Round number is negative or round count is exceeds compare to define interview round");
            }
            CandidateInterviewModel is_interview_exist = await getInterview(interview.fk_interview_scheduler_id, interview.round_number);
            if (is_interview_exist != null)
            {
                throw new Exception("Already Interview is there");
            }
            await _context.Interviews.AddAsync(interview);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> validateInterviewRound(CandidateInterviewModel interviewModel)
    {
        try
        {
            InterviewSchedulerModel interviewScheduler = await _context.InterviewSchedulers.FindAsync(interviewModel.fk_interview_scheduler_id);
            if (interviewScheduler == null || interviewModel.round_number <= 0)
            {
                return false;
            }
            if (interviewModel.interview_type == InterviewType.hr && interviewScheduler.no_of_hr_round >= interviewModel.round_number)
            {
                return true;
            }
            if (interviewModel.interview_type == InterviewType.tech && interviewScheduler.no_of_tech_round >= interviewModel.round_number)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}