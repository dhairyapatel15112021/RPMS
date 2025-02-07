using RecruitmentSystem.Models;

namespace RecruitmentSystem.dto;

public class HoldPostion{
    public String comments {get;set;}

    public PositionStatus? is_open {get;set;} = PositionStatus.hold;
}