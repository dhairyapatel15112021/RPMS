using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class ReviewerPanelModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_reviwer_panel_id { get; set; }

    [Required]
    public int fk_emp_review_id { get; set; }

    [Required]
    public int fk_position_review_id { get; set; }

    [Required]
    public DateTime review_deadline { get; set; }

    public EmployeesModel? employees { get; set; }
    public PositionModel? position { get; set; }
}