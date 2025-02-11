using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class InterviewFeedbackModel
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_interview_feedback_id{get;set;}

    [Required]
    public int fk_emp_id{get;set;}

    [Required]
    public string comments{get;set;}

    [Required]
    public int fk_interview_id{get;set;}

    public EmployeesModel? Employees{get;set;}

    public CandidateInterviewModel? Intervies {get;set;}
}