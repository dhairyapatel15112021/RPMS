using Microsoft.AspNetCore.JsonPatch;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Position;

public interface IPositionService
{

    Task<Boolean> createOpening(PositionCreatedto position);
    Task<bool> holdOpening(int positionId, JsonPatchDocument<HoldPostion> holdPosition);
    Task<bool> closeOpening(int positionId, JsonPatchDocument<ClosePostion> closePosition);
    Task<bool> openOpening(int positionId);
    Task<bool> updateOpening(int positionId, PositionModel position);
    
    Task<List<PositionDTO>> getAllOpenings();
    Task<List<PositionDTO>> getAllOpenOpenings();
    Task<PositionModel> getPosition(int positionId);
    Task<PositionModel> getPosition(string position_title);
}