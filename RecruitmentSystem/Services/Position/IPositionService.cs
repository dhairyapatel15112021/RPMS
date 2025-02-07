using Microsoft.AspNetCore.JsonPatch;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Position;

public interface IPositionService{

    Task<Boolean> createOpening(PositionModel position);
    Task<bool> holdOpening(int positionId, JsonPatchDocument<HoldPostion> holdPosition);

     Task<bool> closeOpening(int positionId, JsonPatchDocument<ClosePostion> closePosition);

     Task<bool> openOpening(int positionId);

}