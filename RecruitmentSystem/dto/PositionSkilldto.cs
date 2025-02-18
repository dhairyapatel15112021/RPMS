namespace RecruitmentSystem.dto;

public class PositionSkilldto
{
    public List<int> minimumSkill { get; set; } = new();
    public List<int> preferedSkill { get; set; } = new();
    public int positionId {get;set;}
}