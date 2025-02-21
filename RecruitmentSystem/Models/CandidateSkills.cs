using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class CandidateSkillModel
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_candidate_skill_id{get;set;}

    public string skill_name{get;set;}

    public int skill_experience{get;set;}

    public int fk_candidate_id{get;set;}

    public CandidateModel? candidate{get;set;}

}