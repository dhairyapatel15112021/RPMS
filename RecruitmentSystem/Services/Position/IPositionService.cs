using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Position;

public interface IPositionService{

    Boolean createOpening(PositionModel position);
}