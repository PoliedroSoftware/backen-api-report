
namespace Poliedro.Report.Application.SalesReport.Dtos
{
    public record SalesReportDayDto(
        DateTime Date,
        int NumberMonth,
        string Month,
        int Year,
        decimal Sale
        );
    
    
}
