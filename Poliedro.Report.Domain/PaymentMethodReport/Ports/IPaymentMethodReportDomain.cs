using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Domain.PaymentMethodReport.Entity;

namespace Poliedro.Report.Domain.PaymentMethodReport.Ports
{
    public interface IPaymentMethodReportDomain
    {
        Task<Result<PaymentMethodReportEntity, Error>> GetAllAsync(CancellationToken cancellationToken, PaginationParams paginationParams);
    }
}
