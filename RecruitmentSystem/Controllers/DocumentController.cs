using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Document;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/document")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService documentService;

    public DocumentController(IDocumentService documentService)
    {
        this.documentService = documentService;
    }

    [HttpPost("add")]
    public async Task<ActionResult> addDocument([FromBody] DocuementModel docuement)
    {
        try
        {
            if (docuement.document_type.Trim() == "" || docuement.document_file_path.Trim() == "" || docuement.fk_candidate_id == 0)
            {
                throw new Exception("Please Enter Valid Data");
            }
            bool is_saved = await documentService.addDocument(docuement);
            if (!is_saved)
            {
                throw new Exception("Something went wrong! document not saved");
            }
            return Ok("Document Saved");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{documentId}")]
    public async Task<ActionResult> updateDocument(int documentId, [FromBody] DocuementModel docuement)
    {
        try
        {
            if (documentId != docuement.pk_document_id)
            {
                throw new Exception("Both Id's should be same");
            }
            bool is_updated = await documentService.updateDocument(documentId, docuement);
            if (!is_updated)
            {
                throw new Exception("Document is not Updated");
            }
            return Ok("Document Upated Sucessfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }
    }

    [HttpPatch("change/{documentId}")]
    public async Task<ActionResult> updateDocumentField(int documentId,[FromBody] JsonPatchDocument<DocuementModel> document)
    {
        try
        {
            if (documentId == 0)
            {
                return BadRequest("Please Enter documentId");
            }
            bool is_updated = await documentService.updateDocumentField(documentId, document,new ModelStateDictionary());
            if (!is_updated)
            {
                return BadRequest();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}