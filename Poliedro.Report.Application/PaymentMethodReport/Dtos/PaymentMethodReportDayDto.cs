

namespace Poliedro.Report.Application.PaymentMethodReport.Dtos
{
    public record PaymentMethodReportDayDto(

       DateTime Date,
       string PaymentMethod,
       decimal TotalPaid,
       decimal TotalSales,
       long InvoiceQuantity,
       decimal TotalSalesDay
 );
}
