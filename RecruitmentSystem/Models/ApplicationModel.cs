using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RecruitmentSystem.Models;

public class ApplicationModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_application_id { get; set; }

    public string? remarks { get; set; }

    public string? comments { get; set; }

    [Required]
    public DateTime application_date { get; set; } = DateTime.Now;

    public DateTime? joining_date;

    [Required]
    public int fk_position_id { get; set; }

    [Required]
    public int fk_candidate_id { get; set; }

    [Required]
    [DefaultValue(ApplicationStatus.applied)]
    public ApplicationStatus applicationStatus { get; set; } = ApplicationStatus.applied;

    public CandidateModel? candidate { get; set; }

    public PositionModel? position { get; set; }
}