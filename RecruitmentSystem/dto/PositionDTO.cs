using RecruitmentSystem.Models;

namespace RecruitmentSystem.dto;

public class PositionDTO{

    public int pk_position_id {get;set;}
    public String position_title {get;set;}
    public String position_description {get;set;}
    public String position_min_experience {get;set;}
    public PositionStatus is_open {get;set;}
    public String? comments {get;set;}
    public String position_level {get;set;}
    public String position_location {get;set;}
    public DateTime? position_creation_date {get;set;}
    public String? salary_range {get;set;}
    public String qualification {get;set;}
    public string emp_name {get;set;}
    public string? candidate_name {get;set;}

}