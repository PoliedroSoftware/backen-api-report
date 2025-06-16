
using System.ComponentModel.DataAnnotations;

namespace Poliedro.Report.Domain.SalesReport.Entities
{
    public class SalesReportYearEntity
    {
        [Key]
        public int Year { get; set; }
        public decimal Sale { get; set; }
    }
}
