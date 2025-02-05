using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class RoleModel
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_role_id { get; set; }

    [Required]
    public string role_type { get; set; }

    public ICollection<EmpRoleMapModel>? RoleMap { get; }
}