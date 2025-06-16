using Poliedro.Report.Application.PaymentMethodReport.Dtos;

public record PaymentMethodReportDto(

    List<PaymentMethodReportYearDto> ForYear,
    List<PaymentMethodReportMonthDto> ForMonth,
    List<PaymentMethodReportDayDto> ForDay
);
