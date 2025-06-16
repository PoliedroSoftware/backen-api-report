
namespace Poliedro.Report.Application.UtilityReport.Dtos
{
    public record UtilityReportMonthDto(
        int Year,
        string Month,
        decimal Utility
        );
}
