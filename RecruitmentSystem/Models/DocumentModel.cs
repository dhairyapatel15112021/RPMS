using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class DocuementModel{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_document_id {get;set;}

    [Required]
    public string document_type {get;set;}

    [Required]
    public string document_file_path {get;set;}

    public DateTime? verified_at {get;set;}

    [DefaultValue(false)]
    public bool document_verification_status {get;set;} = false;

    public DateTime uploaded_at {get;set;} = DateTime.Now;

    public int? fk_candidate_id {get;set;}

    public CandidateModel? candidate {get;set;}

    public int? fk_emp_id {get;set;}
    public EmployeesModel? employees {get;set;}
}