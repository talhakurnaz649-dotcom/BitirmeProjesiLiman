using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using BitirmeProjesiLiman.Core.Entities;
using System;

namespace BitirmeProjesiLiman.Service.Export
{
    public class PdfExportService
    {
        public byte[] GenerateInvoicePdf(PortInvoices invoice)
        {
            // QuestPDF Lisanslama (Ücretsiz topluluk lisansı)
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    // 1. PDF Başlığı (Header)
                    page.Header().Text("PORTMASTER LİMAN İŞLETMELERİ A.Ş.")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    // 2. PDF Gövdesi (Content)
                    page.Content().PaddingVertical(20).Column(column =>
                    {
                        column.Spacing(10);
                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                        column.Item().Text($"Fatura No: #INV-{invoice.Id}").Bold().FontSize(14);
                        column.Item().Text($"Tarih: {invoice.InvoiceDate:dd.MM.yyyy}");
                        column.Item().Text($"Gemi Acente ID: {invoice.VesselId}");
                        column.Item().Text($"Rıhtım Rezervasyon ID: {invoice.BerthAllocationId}");
                        
                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                        // Fatura Hizmet Kalemi Tablosu
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // Açıklama
                                columns.RelativeColumn(1); // Tutar
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Lighten5).Padding(5).Text("Hizmet Açıklaması").Bold();
                                header.Cell().Background(Colors.Blue.Lighten5).Padding(5).Text("Tutar").Bold();
                            });

                            table.Cell().Padding(5).Text("Rıhtım Hizmeti ve Liman Liman Yanaşma Bedeli");
                            table.Cell().Padding(5).Text($"{invoice.TotalAmount:N2} TL");
                        });

                        column.Item().AlignRight().Text($"Genel Toplam: {invoice.TotalAmount:N2} TL")
                            .Bold().FontSize(16).FontColor(Colors.Green.Darken2);
                    });

                    // 3. PDF Alt Bilgisi (Footer)
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
