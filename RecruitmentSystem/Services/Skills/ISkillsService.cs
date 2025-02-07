using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Skills;

public interface ISkillsService{

    Task<Boolean> createSkill(SkillsModel skil);
}