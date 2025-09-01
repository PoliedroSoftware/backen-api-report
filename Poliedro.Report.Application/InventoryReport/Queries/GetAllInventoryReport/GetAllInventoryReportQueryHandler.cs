using AutoMapper;
using MediatR;
using Poliedro.Billing.Application.InventoryReport.Dtos;
using Poliedro.Billing.Domain.Common.Pagination;
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
) : IRequestHandler<GetAllInventoryReportQuery, Result<PaginationResponse<InventoryReportDto>, Error>>
{

    public async Task<Result<PaginationResponse<InventoryReportDto>, Error>>
        Handle(GetAllInventoryReportQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"inventoryReport_{request.paginationParams.PageNumber}_{request.paginationParams.PageSize}";

        var cachedData = await redisService.GetCacheAsync<PaginationResponse<InventoryReportDto>>(cacheKey);
        if (cachedData is not null)
        {
            return Result<PaginationResponse<InventoryReportDto>, Error>.Success(cachedData);
        }

        var result = await inventoryReportDomain.GetAllAsync(cancellationToken, request.paginationParams);

        if (!result.IsSuccess || result.Value == null)
            return result.Error!;

        var dtoList = mapper.Map<List<InventoryReportDto>>(result.Value.Data);

        var paginationResponse = new PaginationResponse<InventoryReportDto>
        {
            Data = dtoList,
            TotalRows = result.Value.TotalRows,
            TotalPages = result.Value.TotalPages
        };

        await redisService.SetCacheAsync(cacheKey, paginationResponse, TimeSpan.FromMinutes(1440));

        return Result<PaginationResponse<InventoryReportDto>, Error>.Success(paginationResponse);
    }
}
