using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Skills;

public interface ISkillsService{

    Task<Boolean> createSkill(SkillsModel skil);
    Task<List<SkillsModel>> getAllSkills();
    Task<bool> updateSkill(int id , SkillsModel skills);

}