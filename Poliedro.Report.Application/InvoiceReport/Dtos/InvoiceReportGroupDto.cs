


namespace Poliedro.Report.Application.InvoiceReport.Dtos
{
    public record InvoiceReportGroupDto(
        string ContactName,
        List<InvoiceDetailReportDto> Products);

}
