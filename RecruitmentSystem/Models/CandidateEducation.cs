using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecruitmentSystem.Models;

public class CandidateEducation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_candidate_education_id{get;set;}

    public string institute_name{get;set;}

    public string qualification_type{get;set;}

    public int passing_year{get;set;}

    public double percentage_score{get;set;}

    public int fk_candidate_id{get;set;}

    public CandidateModel? candidate{get;set;}
}