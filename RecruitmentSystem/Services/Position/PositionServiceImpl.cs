using RecruitmentSystem.Data;
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
    public Boolean createOpening(PositionModel position)
    {
        try
        {
            _context.Position.Add(position);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }
}