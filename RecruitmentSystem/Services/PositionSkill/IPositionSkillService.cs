using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.PositionSkill;
public interface IPositionSkillService {
    Boolean addSkillToPosition(PositionSkillMapModel positionSkillMapModel);
}