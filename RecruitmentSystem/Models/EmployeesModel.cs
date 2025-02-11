using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class EmployeesModel{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_emp_id {get;set;}

    [Required]
    public String emp_name {get;set;}

    [Required]
    [EmailAddress]
    public String emp_email {get;set;}

    [Required]
    [Length(10,10)]
    public String emp_contact_number {get;set;}

    [Required]
    public String emp_designation {get;set;}

    [Required]
    public String emp_password {get;set;}

    [Required]
    public DateTime emp_joining_date {get;set;}

    public ICollection<PositionModel>? positions {get;}

    public ICollection<EmpRoleMapModel>? RoleMap {get;}

    public ICollection<DocuementModel>? Document {get;}

    public ICollection<ReviewerPanelModel>? Reviews {get;}

    public ICollection<InterviewPanelModel>? Interviews {get;}

    public ICollection<ReviewFeedbackModel>? ReviewFeedbacks {get;}

    public ICollection<InterviewFeedbackModel>? InterviewFeedbacks {get;}
}