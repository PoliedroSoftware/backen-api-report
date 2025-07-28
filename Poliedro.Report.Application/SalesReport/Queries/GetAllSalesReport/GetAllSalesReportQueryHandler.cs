using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.SalesReport.Dtos;
using Poliedro.Report.Domain.SalesReport.Ports;
using Poliedro.Report.Application.Ports.Redis;

namespace Poliedro.Report.Application.SalesReport.Queries.GetAllSalesReport;

public class GetAllSalesReportQueryHandler(
    ISalesReportDomain SalesReportDomain,
    IMapper mapper,
    IRedisService redisService
) : IRequestHandler<GetAllSalesReportQuery, Result<SalesReportDto, Error>>
{
    public async Task<Result<SalesReportDto, Error>> Handle(
        GetAllSalesReportQuery request,
        CancellationToken cancellationToken)
    {
        string cacheKey = $"salesReport_{request.PaginationParams.PageNumber}_{request.PaginationParams.PageSize}";
        var cachedData = await redisService.GetCacheAsync<SalesReportDto>(cacheKey);
        if (cachedData is not null)
            return cachedData;

        var result = await SalesReportDomain.GetAllAsync(cancellationToken, request.PaginationParams);

        if (!result.IsSuccess || result.Value is null)
            return result.Error!;

        var response = new SalesReportDto(new(), new(), new())
        {
            ForYear = mapper.Map<List<SalesReportYearDto>>(result.Value.ForYear),
            ForMonth = mapper.Map<List<SalesReportMonthDto>>(result.Value.ForMonth),
            ForDay = mapper.Map<List<SalesReportDayDto>>(result.Value.ForDay),
        };

        await redisService.SetCacheAsync(cacheKey, response, TimeSpan.FromMinutes(10));

        return response;
    }
}
