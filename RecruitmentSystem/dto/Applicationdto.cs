using RecruitmentSystem.Models;

namespace RecruitmentSystem.dto;

public class Applicationdto
{

    public ApplicationModel application;
    public string? candidate_name;
    public string? candidate_email;
    public string? cv_path;
    public List<InterviewSchedulerModel>? interview;
}