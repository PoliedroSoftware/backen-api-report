

namespace Poliedro.Report.Domain.PaymentMethodReport.Entity
{
    public class PaymentMethodReportEntity
    {
        public List<PaymentMethodReportYearEntity> ForYear { get; set; } = new();
        public List<PaymentMethodReportMonthEntity> ForMonth { get; set; } = new();
        public List<PaymentMethodReportDayEntity> ForDay { get; set; } = new();
    }
}
