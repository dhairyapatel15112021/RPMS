using Azure;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Document;

public class DocumentServiceImpl : IDocumentService
{

    private readonly ApplicationDbContext _context;

    public DocumentServiceImpl(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<DocuementModel> getDocument(int candidateId, string docuement_type)
    {
        return await _context.Docuements.FirstOrDefaultAsync(d => d.fk_candidate_id == candidateId && d.document_type == docuement_type);
    }

    public async Task<bool> addDocument(DocuementModel document)
    {
        try
        {
            DocuementModel is_docuement = await getDocument(document.fk_candidate_id, document.document_type);
            if (is_docuement != null)
            {
                throw new Exception("Document is already exist");
            }
            PositionModel position = await _context.Position.FirstOrDefaultAsync(p => p.fk_candidate_key == document.fk_candidate_id && p.is_open == PositionStatus.close);
            if (position == null)
            {
                throw new Exception("you are not yet selected, only selected candidate can upload document");
            }
            await _context.Docuements.AddAsync(document);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<DocuementModel> getDocumentById(int documentId)
    {
        return await _context.Docuements.FindAsync(documentId);
    }

    public async Task<bool> updateDocument(int documentId, DocuementModel docuement)
    {
        try
        {
            DocuementModel is_document_exist = await getDocumentById(documentId);
            if (is_document_exist == null)
            {
                throw new Exception("Document not found");
            }
            _context.Entry(is_document_exist).CurrentValues.SetValues(docuement);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> updateDocumentField(int documentId, JsonPatchDocument<DocuementModel> patchDocument, ModelStateDictionary ModelState)
    {
        try
        {
            DocuementModel is_document_exist = await getDocumentById(documentId);
            if (is_document_exist == null)
            {
                throw new Exception("Docuement Not Found");
            }
            patchDocument.ApplyTo(is_document_exist);
            if (!ModelState.IsValid)
            {
                throw new Exception("Model Validation failed");
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return true;
        }
    }


}