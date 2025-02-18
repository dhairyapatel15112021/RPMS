using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class SkillsModel{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_skills_id {get;set;}

    [Required]
    public String skills_name {get;set;}

    [Required]
    public String skills_description {get;set;}


    public ICollection<PositionSkillMapModel>? PositionSkill {get;} 

}