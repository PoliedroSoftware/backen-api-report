using System.ComponentModel.DataAnnotations;

namespace Poliedro.Report.Domain.UtilityReport.Entity
{
    public class UtilityReportMonthEntity
    {
        [Key]
        public int Year { get; set; }

        public string Month { get; set; } = string.Empty;

        public decimal Utility { get; set; }
    }
}
