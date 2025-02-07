using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Position;

namespace RecruitmentSystem.Controllers.Services;

public class PositionServiceImpl : IPositionService
{
    private ApplicationDbContext _context;

    public PositionServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Boolean> createOpening(PositionModel position)
    {
        try
        {
            await _context.Position.AddAsync(position);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }

    public async Task<List<PositionModel>> getAllPosition()
    {
        return await _context.Position.ToListAsync();
    }

    public async Task<PositionModel> getPosition(int positionId)
    {
        return await _context.Position.FindAsync(positionId);
    }

    public async Task<bool> closeOpening(int positionId, JsonPatchDocument<ClosePostion> closePosition)
    {
        try
        {
            PositionModel position = await getPosition(positionId);

            if (position == null)
            {
                throw new Exception("Position Not Found");
            }
            if (position.is_open == PositionStatus.close)
            {
                throw new Exception("Position is already close");
            }

            var patchDocument = new ClosePostion();
            closePosition.ApplyTo(patchDocument);

            if (patchDocument.comments == null || (patchDocument.comments.Trim() == "" && patchDocument.fk_candidate_id == null))
            {
                throw new Exception("You Have to give reason or you have to select candidate for the position");
            }

            position.is_open = PositionStatus.close;
            if (patchDocument.comments != null && patchDocument.comments.Trim() != "")
                position.comments = patchDocument.comments;

            if (patchDocument.fk_candidate_id != null && patchDocument.fk_candidate_id != 0)
                position.fk_candidate_key = patchDocument.fk_candidate_id;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> holdOpening(int positionId, JsonPatchDocument<HoldPostion> holdPosition)
    {
        try
        {
            PositionModel position = await getPosition(positionId);
            if (position == null)
            {
                throw new Exception("Position Not Found");
            }
            if (position.is_open == PositionStatus.hold)
            {
                throw new Exception("Position is already on hold");
            }

            var patchDocument = new HoldPostion();
            holdPosition.ApplyTo(patchDocument);

            position.is_open = PositionStatus.hold;
            if (patchDocument.comments != null && patchDocument.comments.Trim() == null)
                position.comments = patchDocument.comments;
            else
                throw new Exception("Comments is Compalusary");

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> openOpening(int positionId)
    {
        try
        {
            PositionModel position = await getPosition(positionId);
            if (position == null)
            {
                throw new Exception("Position Not Found");
            }
            if (position.is_open == PositionStatus.open)
            {
                throw new Exception("Position is already open");
            }

            position.is_open = PositionStatus.open;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}