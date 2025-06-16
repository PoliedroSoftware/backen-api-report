using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Domain.UtilityReport.Entity;

namespace Poliedro.Report.Domain.UtilityReport.Ports
{
    public interface IUtilityReportDomain
    {
        Task<Result<UtilityReportEntity, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams);
    }
}
