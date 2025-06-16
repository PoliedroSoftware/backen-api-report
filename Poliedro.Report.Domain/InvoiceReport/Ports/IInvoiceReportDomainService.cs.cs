

using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Report.Domain.InvoiceReport.Entities;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Report.Domain.InvoiceReport.Ports
{
    public interface IInvoiceReportDomainService
    {
        Task<Result<IEnumerable<InvoiceReportEntity>, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams);
    }
}
