
namespace Poliedro.Report.Application.PaymentMethodReport.Dtos
{
    public record PaymentMethodReportYearDto(

       int Year,
       string PaymentMethod,
       decimal TotalPaid,
       decimal TotalSales,
       long InvoiceQuantity
);
}


