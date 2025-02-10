using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class PositionModel{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_position_id {get;set;}

    [Required]
    public String position_title {get;set;}

    [Required]
    public String position_description {get;set;}

    [Required]
    public String position_min_experience {get;set;}

    [Required]
    [DefaultValue(PositionStatus.open)]
    public PositionStatus is_open {get;set;} = PositionStatus.open;

    public String? comments {get;set;}

    public String position_level {get;set;}

    [Required]
    public String position_location {get;set;}

    [Required]
    public DateTime? position_creation_date {get;set;} = DateTime.Now;

    public String? salary_range {get;set;}

    [Required]
    public String qualification {get;set;}

    [Required]
    public int fk_emp_id {get;set;}

    public EmployeesModel? Employees {get;set;}

    public int? fk_candidate_key {get;set;}
    public CandidateModel? Candidate {get;set;}

    public ICollection<PositionSkillMapModel>? PositionSkill {get;} 

    public ICollection<ReviewerPanelModel>? Reviews {get;}

    public ICollection<InterviewPanelModel>? Interviews {get;}

    public ICollection<ApplicationModel>? Applications {get;}
}