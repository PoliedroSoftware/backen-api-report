using AutoMapper;
using MediatR;
using Poliedro.Billing.Application.InventoryReport.Dtos;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.InventoryReport.DomainInventoryReport;
using Poliedro.Report.Application.Ports.Redis;

namespace Poliedro.Billing.Application.InventoryReport.Queries.GetAllInventoryReport;

public class GetAllInventoryReportQueryHandler
(
    IInventoryReportDomainInventoryReport inventoryReportDomain,
    IMapper mapper,
    IRedisService redisService
) : IRequestHandler<GetAllInventoryReportQuery, Result<IEnumerable<InventoryReportDto>, Error>>
{

    public async Task<Result<IEnumerable<InventoryReportDto>, Error>>
        Handle(GetAllInventoryReportQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"inventoryReport_{request.paginationParams.PageNumber}_{request.paginationParams.PageSize}";

        var cachedData = await redisService.GetCacheAsync<IEnumerable<InventoryReportDto>>(cacheKey);
        if (cachedData is not null)
        {
            return Result<IEnumerable<InventoryReportDto>, Error>.Success(cachedData);
        }

        var result = await inventoryReportDomain.GetAllAsync(cancellationToken, request.paginationParams);

        if (!result.IsSuccess && result.Value != null)
            return result.Error!;

        var dtoList = mapper.Map<List<InventoryReportDto>>(result.Value);

        await redisService.SetCacheAsync(cacheKey, dtoList, TimeSpan.FromMinutes(1440));

        return dtoList;
    }
}
