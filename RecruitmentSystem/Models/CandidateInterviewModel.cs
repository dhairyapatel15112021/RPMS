using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class CandidateInterviewModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_interview_id { get; set; }

    [Required]
    public int fk_interview_scheduler_id { get; set; }

    public string? remarks { get; set; }

    [Required]
    public int round_number { get; set; }

    [Required]
    public InterviewType interview_type { get; set; }

    [Required]
    public string interview_link {get;set;}

    [Required]
    public bool IsDone { get; set; }

    [Required]
    public int Index { get; set; }

    [Required]
    public DateTime interview_date { get; set; }

    [Required]
    public TimeSpan interview_time { get; set; }

    public InterviewSchedulerModel? InterviewScheduler { get; set; }

    public ICollection<InterviewFeedbackModel>? InterviewFeedbacks { get; }
}