using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class EmpRoleMapModel
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_role_map_id { get; set; }

    [Required]
    public int fk_emp_id { get; set; }

    public EmployeesModel? Employees { get; set; }

    [Required]
    public int fk_role_id { get; set; }
    public RoleModel? Role { get; set; }
}