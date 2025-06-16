

using System.ComponentModel.DataAnnotations;

namespace Poliedro.Report.Domain.SalesReport.Entities
{
    public class SalesReportMonthEntity
    {
        [Key]
        public int NumberMonth { get; set; }
        public string Month { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Sale { get; set; }
    }
}
