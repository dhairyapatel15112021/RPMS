using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Document;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/document")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService documentService;

    public DocumentController(IDocumentService documentService){
        this.documentService = documentService;
    }

    [HttpPost("add")]
    public async Task<ActionResult> addDocument([FromBody] DocuementModel docuement){
        // validation is left.
        try{
            if(docuement.document_type.Trim() == "" || docuement.document_file_path.Trim() == "" || docuement.fk_candidate_id == 0){
                throw new Exception("Please Enter Valid Data");
            }
            bool is_saved = await documentService.addDocument(docuement);
            if(!is_saved){
                throw new Exception("Something went wrong! document not saved");
            }
            return Ok("Document Saved");
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }
}