using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecruitmentSystem.Models;

public class CandidateExperience
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_candidate_experience{get;set;}

    public string company_name{get;set;}

    public int no_of_years{get;set;}

    public string job_profile{get;set;}

    public string job_responsibility{get;set;}

    public int fk_candidate_id{get;set;}
    public CandidateModel? candidate{get;set;}
    
}