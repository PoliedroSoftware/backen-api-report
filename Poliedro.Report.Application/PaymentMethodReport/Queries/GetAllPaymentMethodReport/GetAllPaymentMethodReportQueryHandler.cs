using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.PaymentMethodReport.Dtos;
using Poliedro.Report.Domain.PaymentMethodReport.Ports;
using StackExchange.Redis;
using System.Text.Json;

namespace Poliedro.Report.Application.PaymentMethodReport.Queries.GetAllPaymentMethodReport;

public class GetAllPaymentMethodReportQueryHandler(
    IPaymentMethodReportDomain paymentMethodReportDomain,
    IMapper mapper,
    IConnectionMultiplexer redis
) : IRequestHandler<GetAllPaymentMethodReportQuery, Result<PaymentMethodReportDto, Error>>
{
    public async Task<Result<PaymentMethodReportDto, Error>> Handle(
        GetAllPaymentMethodReportQuery request,
        CancellationToken cancellationToken)
    {
        var db = redis.GetDatabase();
        var cacheKey = $"payment-method-report:{JsonSerializer.Serialize(request.PaginationParams)}";


        var cachedData = await db.StringGetAsync(cacheKey);
        if (cachedData.HasValue)
        {
            var deserialized = JsonSerializer.Deserialize<PaymentMethodReportDto>(cachedData);
            if (deserialized != null)
                return Result<PaymentMethodReportDto, Error>.Success(deserialized);
        }


        var result = await paymentMethodReportDomain.GetAllAsync(cancellationToken, request.PaginationParams);
        if (!result.IsSuccess || result.Value is null)
            return result.Error!;

        var response = new PaymentMethodReportDto(new(), new(), new())
        {
            ForYear = mapper.Map<List<PaymentMethodReportYearDto>>(result.Value.ForYear),
            ForMonth = mapper.Map<List<PaymentMethodReportMonthDto>>(result.Value.ForMonth),
            ForDay = mapper.Map<List<PaymentMethodReportDayDto>>(result.Value.ForDay),
        };


        var jsonData = JsonSerializer.Serialize(response);
        await db.StringSetAsync(cacheKey, jsonData, TimeSpan.FromHours(1));

        return Result<PaymentMethodReportDto, Error>.Success(response);
    }
}
