using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Poliedro.Billing.Application.DraftReport.Dtos;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.DraftReport.DomainDraftReport;
using Poliedro.Billing.Domain.DraftReport.Entities;
using Poliedro.Report.Application.Ports.Redis;

namespace Poliedro.Billing.Application.DraftReport.Queries.GetAllDraftReport;

public class GetAllDraftReportQueryHandler
(
    IDraftReportDomainDraftReport DraftReportDomainDraftReport,
    IMapper mapper,
    IRedisService redisService,
    IConfiguration config
) : IRequestHandler<GetAllDraftReportQuery, Result<IEnumerable<DraftReportDto>, Error>>
{

    public async Task<Result<IEnumerable<DraftReportDto>, Error>>
        Handle(GetAllDraftReportQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"draftReport_{request.paginationParams.PageNumber}_{request.paginationParams.PageSize}";
        var cachedData = await redisService.GetCacheAsync<IEnumerable<DraftReportEntity>>(cacheKey);
        if (cachedData is not null && cachedData.Any())
        {
            var mappedData = mapper.Map<List<DraftReportDto>>(cachedData);
            return Result<IEnumerable<DraftReportDto>, Error>.Success(mappedData);
        }
       
        var result = await DraftReportDomainDraftReport.GetAllAsync(cancellationToken, request.paginationParams);
        await redisService.SetCacheAsync(cacheKey, result.Value, TimeSpan.FromMinutes(double.Parse(config["Redis:ExpirationInMinutes"]!)));
        if (!result.IsSuccess && result.Value != null)
            return result.Error!;

        return mapper.Map<List<DraftReportDto>>(result.Value);
    }
}
