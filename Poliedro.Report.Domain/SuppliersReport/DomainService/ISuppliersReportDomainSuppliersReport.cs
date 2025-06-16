using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.SuppliersReport.Entities;

namespace Poliedro.Billing.Domain.SuppliersReport.DomainSuppliersReport
{
    public interface ISuppliersReportDomainSuppliersReport
    {
        Task<Result<IEnumerable<SuppliersReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams);
    }
}
