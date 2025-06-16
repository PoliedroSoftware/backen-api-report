
using AutoMapper;
using MediatR;
using Poliedro.Billing.Application.DraftReport.Dtos;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.DraftReport.DomainDraftReport;

namespace Poliedro.Billing.Application.DraftReport.Queries.GetAllDraftReport;

public class GetAllDraftReportQueryHandler
(
    IDraftReportDomainDraftReport DraftReportDomainDraftReport,
    IMapper mapper
) : IRequestHandler<GetAllDraftReportQuery, Result<IEnumerable<DraftReportDto>, Error>>
{

    public async Task<Result<IEnumerable<DraftReportDto>, Error>>
        Handle(GetAllDraftReportQuery request, CancellationToken cancellationToken)
    {
        var result = await DraftReportDomainDraftReport.GetAllAsync(cancellationToken, request.paginationParams);

        if (!result.IsSuccess && result.Value != null)
            return result.Error!;

        return mapper.Map<List<DraftReportDto>>(result.Value);
    }
}
