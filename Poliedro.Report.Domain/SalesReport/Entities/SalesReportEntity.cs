

namespace Poliedro.Report.Domain.SalesReport.Entities
{
    public class SalesReportEntity
    {
        public List<SalesReportYearEntity> ForYear { get; set; } = new();
        public List<SalesReportMonthEntity> ForMonth { get; set; } = new();
        public List<SalesReportDayEntity> ForDay { get; set; } = new();
    }
}
