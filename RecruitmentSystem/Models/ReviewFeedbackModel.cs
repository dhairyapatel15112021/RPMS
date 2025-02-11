using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class ReviewFeedbackModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_review_id {get;set;}

    [Required]
    public int fk_application_id{get;set;}

    [Required]
    public int fk_emp_id{get;set;}

    [Required]
    public string comments{get;set;}

    public EmployeesModel? Employees{get;set;}

    public ApplicationModel? application{get;set;}
}