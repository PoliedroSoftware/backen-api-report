
namespace Poliedro.Report.Domain.UtilityReport.Entity;

public class UtilityReportEntity
{
    public List<UtilityReportYearEntity> ForYear { get; set; } = new();
    public List<UtilityReportMonthEntity> ForMonth { get; set; } = new();
    public List<UtilityReportDayEntity> ForDay { get; set; } = new();
}
