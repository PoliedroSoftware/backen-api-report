

namespace Poliedro.Report.Application.SalesReport.Dtos
{
    public record SalesReportDto(
        List<SalesReportYearDto> ForYear,
        List<SalesReportMonthDto> ForMonth,
        List<SalesReportDayDto> ForDay
        ); 
}
