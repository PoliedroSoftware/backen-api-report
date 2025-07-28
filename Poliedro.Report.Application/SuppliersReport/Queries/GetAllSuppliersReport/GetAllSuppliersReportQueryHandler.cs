using AutoMapper;
using MediatR;
using Poliedro.Billing.Application.SuppliersReport.Dtos;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.SuppliersReport.DomainSuppliersReport;
using Poliedro.Report.Application.Ports.Redis;

namespace Poliedro.Billing.Application.SuppliersReport.Queries.GetAllUtilityReport;

public class GetAllSuppliersReportQueryHandler(
    ISuppliersReportDomainSuppliersReport suppliersreportDomainSuppliersReport,
    IMapper mapper,
    IRedisService redisService
) : IRequestHandler<GetAllSuppliersReportQuery, Result<IEnumerable<SuppliersReportDto>, Error>>
{
    public async Task<Result<IEnumerable<SuppliersReportDto>, Error>>
        Handle(GetAllSuppliersReportQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"suppliersReport_{request.PaginationParams.PageNumber}_{request.PaginationParams.PageSize}";
        var cachedData = await redisService.GetCacheAsync<IEnumerable<SuppliersReportDto>>(cacheKey);
        if (cachedData is not null)
            return cachedData.ToList();

        var result = await suppliersreportDomainSuppliersReport.GetAllAsync(cancellationToken, request.PaginationParams);
        if (!result.IsSuccess || result.Value is null)
            return result.Error!;

        var mapped = mapper.Map<List<SuppliersReportDto>>(result.Value);
        await redisService.SetCacheAsync(cacheKey, mapped, TimeSpan.FromMinutes(5));

        return mapped;
    }
}
