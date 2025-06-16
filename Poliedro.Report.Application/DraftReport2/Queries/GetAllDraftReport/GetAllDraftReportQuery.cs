using MediatR;
using Poliedro.Billing.Application.DraftReport.Dtos;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;

namespace Poliedro.Billing.Application.DraftReport.Queries.GetAllDraftReport
{
    public record GetAllDraftReportQuery(PaginationParams paginationParams) : IRequest<Result<IEnumerable<DraftReportDto>, Error>>;

}

