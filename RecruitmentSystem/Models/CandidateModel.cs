using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentSystem.Models;

public class CandidateModel{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int pk_candidate_id {get;set;}

    [Required]
    public String candidate_name {get;set;}

    [Required]
    [Length(10,10)]
    public String candidate_contact_number {get;set;}

    [Required]
    [EmailAddress]
    public String candidate_email{get;set;}

    [Required]
    public String candidate_password{get;set;}

    public String candidate_linkdien{get;set;}

    public Boolean document_verification_status{get;set;}

    public String ssc_marksheet{get;set;}

    public String hsc_marksheet{get;set;}

    public String aadhar_card{get;set;}

    public String pan_card{get;set;}

    public String cancle_cheque{get;set;}

    public PositionModel? Position {get; set;}
}