

using System.ComponentModel.DataAnnotations;

namespace Poliedro.Report.Domain.PaymentMethodReport.Entity
{
    public class PaymentMethodReportDayEntity
    {
     [Key]
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal TotalPaid { get; set; }
        public decimal TotalSales { get; set; }
        public long InvoiceQuantity { get; set; }
        public decimal TotalSalesDay { get; set; }

    }
}
