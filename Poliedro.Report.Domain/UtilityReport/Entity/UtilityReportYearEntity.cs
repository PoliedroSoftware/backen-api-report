using System.ComponentModel.DataAnnotations;

namespace Poliedro.Report.Domain.UtilityReport.Entity
{
    public class UtilityReportYearEntity
    {
        [Key]
        public int Year { get; set; }

        public decimal Utility { get; set; }
    }
}
