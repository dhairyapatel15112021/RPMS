using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.InterviewScheduler;

public interface IInterviewSchedulerService
{
    Task<bool> scheduleInterview(InterviewScheduledto interviewdto);

    Task<InterviewScheduledto> getScheduledInterview(int applicationId,bool isHr);

    Task<InterviewSchedulerModel> getScheduledInterviewById(int interviewSchedulerId);
}