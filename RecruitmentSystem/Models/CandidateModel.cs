using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class CandidateModel
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_candidate_id { get; set; }

    [Required]
    public String candidate_name { get; set; }

    [Required]
    [Length(10, 10)]
    public String candidate_contact_number { get; set; }

    [Required]
    [EmailAddress]
    public String candidate_email { get; set; }

    [Required]
    public String candidate_password { get; set; }

    public String? candidate_linkdien { get; set; }

    [DefaultValue("candidate")]
    public String role { get; set; } = "candidate";
    public PositionModel? Position { get; set; }

    public ICollection<DocuementModel>? Document { get; }

    public ICollection<ApplicationModel>? Applications {get;}
    
}