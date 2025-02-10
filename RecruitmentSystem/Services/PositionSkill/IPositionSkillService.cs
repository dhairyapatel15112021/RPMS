using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.PositionSkill;
public interface IPositionSkillService {
    Task<bool> addSkillToPosition(PositionSkillMapModel positionSkillMapModel);

    Task<PositionSkillMapModel> getSkillPositionByPositionIdAndSkillId(int positionId, int skillId);

    Task<bool> removeSkillToPosition(int positionId, int skillId);
}