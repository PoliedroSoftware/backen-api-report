
using MediatR;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Report.Application.InvoiceReport.Dtos;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Report.Application.InvoiceReport.Queries.GetAllInvoiceReport
{
    public record GetAllInvoiceReportQuery(PaginationParams PaginationParams) : IRequest<Result<IEnumerable<InvoiceReportGroupDto>, Error>>;
    
}
