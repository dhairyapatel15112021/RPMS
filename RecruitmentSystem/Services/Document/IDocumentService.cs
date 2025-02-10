using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Document;

public interface IDocumentService
{
    Task<bool> addDocument(DocuementModel documentModel);

    Task<DocuementModel> getDocument(int candidateId, string docuement_type);
    Task<DocuementModel> getDocumentById(int documentId);
    Task<bool> updateDocument(int documentId, DocuementModel docuement);

    Task<bool> updateDocumentField(int documentId, JsonPatchDocument<DocuementModel> patchDocument, ModelStateDictionary ModelState);

}