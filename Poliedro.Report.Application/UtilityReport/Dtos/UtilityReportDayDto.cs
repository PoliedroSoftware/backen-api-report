
namespace Poliedro.Report.Application.UtilityReport.Dtos
{
    public record UtilityReportDayDto(
        DateTime Date,
        int Year,
        string Month,
        decimal Utility
        );
}
