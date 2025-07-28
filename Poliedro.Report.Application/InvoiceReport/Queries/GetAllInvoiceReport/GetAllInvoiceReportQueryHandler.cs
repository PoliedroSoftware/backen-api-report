using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.InvoiceReport.Dtos;
using Poliedro.Report.Application.Ports.Redis;
using Poliedro.Report.Domain.InvoiceReport.Ports;

namespace Poliedro.Report.Application.InvoiceReport.Queries.GetAllInvoiceReport
{
    public class GetAllInvoiceReportQueryHandler
    (
        IInvoiceReportDomainService invoiceReportDomainService,
        IMapper mapper,
        IRedisService redisService
    ) : IRequestHandler<GetAllInvoiceReportQuery, Result<IEnumerable<InvoiceReportGroupDto>, Error>>
    {
        public async Task<Result<IEnumerable<InvoiceReportGroupDto>, Error>> Handle(
            GetAllInvoiceReportQuery request,
            CancellationToken cancellationToken)
        {
            string cacheKey = $"invoiceReport_{request.PaginationParams.PageNumber}_{request.PaginationParams.PageSize}";

            var cachedData = await redisService.GetCacheAsync<IEnumerable<InvoiceReportGroupDto>>(cacheKey);
            if (cachedData is not null)
            {
                return Result<IEnumerable<InvoiceReportGroupDto>, Error>.Success(cachedData);
            }

            var result = await invoiceReportDomainService.GetAllAsync(cancellationToken, request.PaginationParams);

            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var grouped = result.Value
                .GroupBy(x => x.Transaccion)
                .Select(g =>
                    new InvoiceReportGroupDto(
                        g.First().ContactName,
                        mapper.Map<List<InvoiceDetailReportDto>>(g.ToList())
                    )
                ).ToList();

            await redisService.SetCacheAsync(cacheKey, grouped, TimeSpan.FromMinutes(1440));

            return Result<IEnumerable<InvoiceReportGroupDto>, Error>.Success(grouped);
        }
    }
}
