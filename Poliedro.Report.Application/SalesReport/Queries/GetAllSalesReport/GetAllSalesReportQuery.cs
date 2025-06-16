

using MediatR;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.SalesReport.Dtos;

namespace Poliedro.Report.Application.SalesReport.Queries.GetAllSalesReport
{
    public record GetAllSalesReportQuery(PaginationParams PaginationParams) : IRequest<Result<SalesReportDto, Error>>
    {
    }
}
