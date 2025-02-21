using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class PositionSkillMapModel{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_position_skill_id{get;set;}

    [Required]
    public int fk_position_id {get;set;}

    [Required]
    public int fk_skills_id {get;set;}

    [Required]
    public Boolean is_min_req_skills {get;set;}

    public PositionModel? Position {get;set;}

    public SkillsModel? Skills {get;set;}

    public ICollection<InterviewFeedbackModel>? InterviewFeedbacks {get;set;}
}