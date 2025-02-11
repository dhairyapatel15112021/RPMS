using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.InterviewScheduler;

public interface IInterviewSchedulerService
{
    Task<InterviewSchedulerModel> scheduleInterview(InterviewSchedulerModel interviewScheduler);

    Task<InterviewSchedulerModel> getScheduledInterview(int applicationId);

    Task<InterviewSchedulerModel> getScheduledInterviewById(int interviewSchedulerId);
}