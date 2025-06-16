

namespace Poliedro.Report.Application.SalesReport.Dtos
{
    public record SalesReportMonthDto(
        int NumberMonth,
        string Month,
        int Year,
        decimal Sale
        );
    
    
}
