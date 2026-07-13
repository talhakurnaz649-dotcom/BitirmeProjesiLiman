using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Core.Entities;
using BitirmeProjesiLiman.Service.Export;
using System;
using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Dapper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ExcelExportService _excelExportService;
        private readonly PdfExportService _pdfExportService;

        public ReportsController(ExcelExportService excelExportService, PdfExportService pdfExportService)
        {
            _excelExportService = excelExportService;
            _pdfExportService = pdfExportService;
        }

        [HttpGet("vessels/excel")]
        public async Task<IActionResult> DownloadVesselExcel()
        {
            var fileContents = await _excelExportService.ExportVesselsToExcelAsync();
            return File(
                fileContents,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Gemi_Raporu.xlsx"
            );
        }

        [HttpGet("invoice/pdf/{id}")]
        public IActionResult DownloadInvoicePdf(int id)
        {
            var tempInvoice = new PortInvoices
            {
                Id = id,
                VesselId = 1,
                BerthAllocationId = 12,
                TotalAmount = 24500,
                PaymentStatus = "Unpaid",
                InvoiceDate = DateTime.Now
            };

            var pdfContents = _pdfExportService.GenerateInvoicePdf(tempInvoice);
            return File(
                pdfContents,
                "application/pdf",
                $"Fatura_INV_{id}.pdf"
            );
        }
    }
}
