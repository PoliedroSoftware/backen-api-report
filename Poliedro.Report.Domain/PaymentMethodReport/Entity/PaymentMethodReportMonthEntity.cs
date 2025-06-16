

using System.ComponentModel.DataAnnotations;

namespace Poliedro.Report.Domain.PaymentMethodReport.Entity
{
    public class PaymentMethodReportMonthEntity
    {
        [Key]

        public int Year { get; set; }
        public int Nmonth { get; set; }
        public string Month { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal TotalPaid { get; set; }
        public decimal TotalSales { get; set; }
        public long InvoiceQuantity { get; set; }
    }
}
