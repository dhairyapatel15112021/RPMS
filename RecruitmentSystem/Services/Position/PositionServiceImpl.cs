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

    // public async Task<List<PositionModel>> getAllPosition()
    // {
    //     return await _context.Position.ToListAsync();
    // }

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

    public async Task<bool> updateOpening(int positionId, PositionModel position)
    {
        try
        {
            PositionModel isPositionExist = await getPosition(positionId);
            if (isPositionExist == null)
            {
                throw new Exception("Position Not Found");
            }
            _context.Entry(isPositionExist).CurrentValues.SetValues(position);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<PositionDTO>> getAllOpenings()
    {
        var allPositions = from position in _context.Position join emp in _context.Employees on position.fk_emp_id equals emp.pk_emp_id join candidate in _context.Candidate on position.fk_candidate_key equals candidate.pk_candidate_id into result from candidate in result.DefaultIfEmpty() select new {emp.emp_name,candidate.candidate_name,position};
        List<PositionDTO> positions = new List<PositionDTO>();

        foreach(var p in allPositions){
            
            PositionDTO dto = new PositionDTO();
            dto.candidate_name = p.candidate_name;
            dto.emp_name = p.emp_name;
            dto.pk_position_id = p.position.pk_position_id;
            dto.position_title = p.position.position_title;
            dto.position_description = p.position.position_description;
            dto.position_min_experience = p.position.position_min_experience;
            dto.is_open = p.position.is_open;
            dto.comments = p.position.comments;
            dto.position_level = p.position.position_level;
            dto.position_location = p.position.position_location;
            dto.position_creation_date = p.position.position_creation_date;
            dto.salary_range = p.position.salary_range;
            dto.qualification = p.position.qualification;

            positions.Add(dto);
        }
       
        return positions;
    }

    public async Task<List<PositionModel>> getAllOpenOpenings()
    {
        return await _context.Position.Where(p => p.is_open == PositionStatus.open).ToListAsync();
    }
}