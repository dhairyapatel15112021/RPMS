using RecruitmentSystem.Data;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Document;

public class DocumentServiceImpl : IDocumentService{

    private readonly ApplicationDbContext _context;

    public DocumentServiceImpl(ApplicationDbContext context){
        this._context = context;
    }

    public async Task<bool> addDocument(DocuementModel documentModel){
        try{
            await _context.Docuements.AddAsync(documentModel);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}