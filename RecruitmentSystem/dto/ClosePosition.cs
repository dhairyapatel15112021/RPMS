using RecruitmentSystem.Models;

namespace RecruitmentSystem.dto;

public class ClosePostion
{
    public string comments { get; set; }

    public PositionStatus? is_open { get; set; } = PositionStatus.close;

    public int fk_candidate_id { get; set; }
}