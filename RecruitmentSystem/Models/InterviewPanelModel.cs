using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class InterviewPanelModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_interview_panel_id { get; set; }

    [Required]
    public int fk_emp_interview_id { get; set; }

    [Required]
    public int fk_position_interview_id { get; set; }

    public EmployeesModel? employees { get; set; }
    public PositionModel? position { get; set; }
}