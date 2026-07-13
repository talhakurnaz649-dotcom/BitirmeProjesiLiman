using ClosedXML.Excel;
using BitirmeProjesiLiman.Core.Repositories;
using BitirmeProjesiLiman.Core.Entities;
using System.IO;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Service.Export
{
    public class ExcelExportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExcelExportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> ExportVesselsToExcelAsync()
        {
            var vessels = await _unitOfWork.Repository<Vessels>().GetAllAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Gemi Listesi");

                // Tablo Başlıkları
                worksheet.Cell("A1").Value = "ID";
                worksheet.Cell("B1").Value = "Gemi Adı";
                worksheet.Cell("C1").Value = "IMO Numarası";
                worksheet.Cell("D1").Value = "Gross Tonaj (GRT)";
                worksheet.Cell("E1").Value = "Bayrak";
                worksheet.Cell("F1").Value = "Acente / Şirket";

                // Başlık Stilini Güzelleştirme
                var headerRange = worksheet.Range("A1:F1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.AirForceBlue;
                headerRange.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var v in vessels)
                {
                    worksheet.Cell(row, 1).Value = v.Id;
                    worksheet.Cell(row, 2).Value = v.Name;
                    worksheet.Cell(row, 3).Value = v.IMO;
                    worksheet.Cell(row, 4).Value = v.GrossTonnage;
                    worksheet.Cell(row, 5).Value = v.Flag;
                    worksheet.Cell(row, 6).Value = v.ShippingCompany;
                    row++;
                }

                worksheet.Columns().AdjustToContents(); // Sütun genişliklerini otomatik ayarla

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
