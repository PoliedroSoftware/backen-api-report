using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.InventoryReport.Entities;

namespace Poliedro.Billing.Domain.InventoryReport.DomainInventoryReport
{
    public interface  IInventoryReportDomainInventoryReport
    {
        Task<Result<IEnumerable<InventoryReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams);
    }
}
