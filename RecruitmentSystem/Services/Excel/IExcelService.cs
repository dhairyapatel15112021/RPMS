namespace RecruitmentSystem.Services.Excel;

public interface IExcelService{

    Task<List<Dictionary<string,string>>> extractData(IFormFile file);
}