
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace RecruitmentSystem.Services.Excel;

public class ExcelServiceImpl : IExcelService
{
    public async Task<List<Dictionary<string, string>>> extractData(IFormFile file)
    {
        var data = new List<Dictionary<string, string>>();

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheets.First();
                var rows = worksheet.RangeUsed().RowsUsed();

                var headerRow = rows.First(); // Assumes the first row is the header row
                var headers = headerRow.Cells().Select(c => c.Value.ToString()).ToList();

                foreach (var row in rows.Skip(1))
                {
                    var rowData = new Dictionary<string, string>();
                    foreach (var cell in row.Cells())
                    {
                        var header = headers[cell.Address.ColumnNumber - 1];
                        rowData[header] = cell.Value.ToString();
                    }
                    data.Add(rowData);
                }
            }
        }

        return data;
    }
}