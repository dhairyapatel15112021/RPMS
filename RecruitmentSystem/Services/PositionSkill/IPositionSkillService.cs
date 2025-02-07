using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.PositionSkill;
public interface IPositionSkillService {
    Task<bool> addSkillToPosition(PositionSkillMapModel positionSkillMapModel);
}