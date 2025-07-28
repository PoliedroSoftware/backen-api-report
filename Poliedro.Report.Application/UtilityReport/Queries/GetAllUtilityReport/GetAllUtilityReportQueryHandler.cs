using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.Ports.Redis;
using Poliedro.Report.Application.UtilityReport.Dtos;
using Poliedro.Report.Domain.UtilityReport.Ports;

namespace Poliedro.Report.Application.UtilityReport.Queries.GetAllUtilityReport
{
    public class GetAllUtilityReportQueryHandler(
        IUtilityReportDomain utilityReportDomain,
        IMapper mapper,
        IRedisService redisService
    ) : IRequestHandler<GetAllUtilityReportQuery, Result<UtilityReportDto, Error>>
    {
        public async Task<Result<UtilityReportDto, Error>> Handle(
            GetAllUtilityReportQuery request,
            CancellationToken cancellationToken)
        {
            string cacheKey = $"utilityReport_{request.PaginationParams.PageNumber}_{request.PaginationParams.PageSize}";

            var cachedData = await redisService.GetCacheAsync<UtilityReportDto>(cacheKey);
            if (cachedData is not null)
                return cachedData;

            var result = await utilityReportDomain.GetAllAsync(cancellationToken, request.PaginationParams);

            if (!result.IsSuccess || result.Value is null)
                return result.Error!;

            var response = new UtilityReportDto(
                ForYear: mapper.Map<List<UtilityReportYearDto>>(result.Value.ForYear),
                ForMonth: mapper.Map<List<UtilityReportMonthDto>>(result.Value.ForMonth),
                ForDay: mapper.Map<List<UtilityReportDayDto>>(result.Value.ForDay)
            );


            await redisService.SetCacheAsync(cacheKey, response, TimeSpan.FromMinutes(30));

            return response;
        }
    }
}
