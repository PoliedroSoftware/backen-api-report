

namespace Poliedro.Report.Application.PaymentMethodReport.Dtos
{
    public record PaymentMethodReportMonthDto(

       int Year,
       int Nmonth,
       string Month,
       string PaymentMethod,
       decimal TotalPaid,
       decimal TotalSales,
       long InvoiceQuantity
);
}
