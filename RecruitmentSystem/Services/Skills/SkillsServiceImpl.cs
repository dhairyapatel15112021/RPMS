using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Skills;

public class SkillsServiceImpl : ISkillsService{
    
    private readonly ApplicationDbContext _context;

    public SkillsServiceImpl(ApplicationDbContext context){
        _context = context;
    }
    public Boolean createSkill(SkillsModel skills){
        try{
            _context.Skills.Add(skills);
            _context.SaveChanges();
            return true;
        }
        catch(Exception ex){
            Console.WriteLine(ex);
            return false;
        }
    }
}