using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class InterviewSchedulerModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_interview_scheduler_id { get; set; }

    [Required]
    public int fk_application_id { get; set; }

    [Required]
    [DefaultValue(1)]
    public int no_of_hr_round { get; set; } = 1;

    [Required]
    [DefaultValue(2)]
    public int no_of_tech_round { get; set; } = 2;

    public string? assesment_link { get; set; }

    public ApplicationModel? application { get; set; }

    public ICollection<CandidateInterviewModel>? CandidateInterview { get; set; }
}