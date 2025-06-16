namespace Poliedro.Report.Application.UtilityReport.Dtos
{
    public record UtilityReportDto(
        List<UtilityReportYearDto> ForYear,
        List<UtilityReportMonthDto> ForMonth,
        List<UtilityReportDayDto> ForDay
        );
}
