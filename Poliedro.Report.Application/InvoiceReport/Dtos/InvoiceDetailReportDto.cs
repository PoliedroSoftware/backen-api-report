

namespace Poliedro.Report.Application.InvoiceReport.Dtos
{
    public record InvoiceDetailReportDto(
        int Id,
        int Transaccion,
        int Code,
        int TypeItemIdentificationId,
        string Description,
        int UnitMeasureId,
        decimal BaseQuantity,
        decimal InvoicedQuantity,
        decimal PriceAmount,
        decimal LineExtensionAmount,
        double Percent,
        decimal TaxAmount,
        decimal UnitPrice
        );
}



