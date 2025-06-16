using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.DraftReport.Entities;

namespace Poliedro.Billing.Domain.DraftReport.DomainDraftReport
{
    public interface IDraftReportDomainDraftReport
    {
        Task<Result<IEnumerable<DraftReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams);
    }
}
