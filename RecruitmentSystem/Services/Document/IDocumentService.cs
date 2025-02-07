using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Document;

public interface IDocumentService{
    Task<bool> addDocument(DocuementModel documentModel);
}