

using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Report.Domain.SalesReport.Entities;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Report.Domain.SalesReport.Ports
{
    public interface ISalesReportDomain
    {
        Task<Result<SalesReportEntity, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams);
    }
}
