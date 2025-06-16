using AutoMapper;
using MediatR;
using Poliedro.Billing.Application.SuppliersReport.Dtos;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.SuppliersReport.DomainSuppliersReport;

namespace Poliedro.Billing.Application.SuppliersReport.Queries.GetAllUtilityReport;

public class GetAllSuppliersReportQueryHandler
(
    ISuppliersReportDomainSuppliersReport suppliersreportDomainSuppliersReport,
    IMapper mapper
) : IRequestHandler<GetAllSuppliersReportQuery, Result<IEnumerable<SuppliersReportDto>, Error>>
{

    public async Task<Result<IEnumerable<SuppliersReportDto>, Error>>
        Handle(GetAllSuppliersReportQuery request, CancellationToken cancellationToken)
    {
        var result = await suppliersreportDomainSuppliersReport.GetAllAsync(cancellationToken, request.PaginationParams);
        if (!result.IsSuccess && result.Value != null)
            return result.Error!;

        return mapper.Map<List<SuppliersReportDto>>(result.Value);
    }
}