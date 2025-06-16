using AutoMapper;
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.InvoiceReport.Dtos;
using Poliedro.Report.Domain.InvoiceReport.Ports;

namespace Poliedro.Report.Application.InvoiceReport.Queries.GetAllInvoiceReport
{
    public class GetAllInvoiceReportQueryHandler
        (
            IInvoiceReportDomainService InvoiceReportDomainService,
            IMapper mapper
        ) : IRequestHandler<GetAllInvoiceReportQuery, Result<IEnumerable<InvoiceReportGroupDto>, Error>>
    {
        public async Task<Result<IEnumerable<InvoiceReportGroupDto>, Error>> Handle(
            GetAllInvoiceReportQuery request,
            CancellationToken cancellationToken)
        {
            var result = await InvoiceReportDomainService.GetAllAsync(cancellationToken, request.PaginationParams);

            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var grouped = result.Value
                .GroupBy(x => x.Transaccion)
                .Select(g =>
                    new InvoiceReportGroupDto(
                        g.First().ContactName,
                        mapper.Map<List<InvoiceDetailReportDto>>(g.ToList())
                    )
                );
            return Result<IEnumerable<InvoiceReportGroupDto>, Error>.Success(grouped);
        }
    }
}