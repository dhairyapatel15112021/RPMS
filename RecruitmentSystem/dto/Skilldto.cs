namespace RecruitmentSystem.dto;

public class Skilldto
{
    public int pk_skills_id { get; set; }
    public String skills_name { get; set; }
    public String skills_description { get; set; }
    public bool? is_min_req_skills { get; set; }
}