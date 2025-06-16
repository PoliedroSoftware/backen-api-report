
using MediatR;
using Poliedro.Report.Application.PaymentMethodReport.Dtos;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Report.Application.PaymentMethodReport.Queries.GetAllPaymentMethodReport
{
    public record GetAllPaymentMethodReportQuery(PaginationParams PaginationParams) : IRequest<Result<PaymentMethodReportDto, Error>>

    {
    
    }
}
