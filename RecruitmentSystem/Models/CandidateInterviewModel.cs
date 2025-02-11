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

    public InterviewSchedulerModel? InterviewScheduler {get;set;}

     public ICollection<InterviewFeedbackModel>? InterviewFeedbacks {get;}
}