using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.PositionSkill;
public interface IPositionSkillService {
    Task<bool> addSkillToPosition(PositionSkilldto positionSkilldto);

    Task<bool> addSkillToWhilePositionCreation(PositionSkilldto positionSkilldto);
    Task<List<Skilldto>> getAllPositionSkills(int positionId);
}