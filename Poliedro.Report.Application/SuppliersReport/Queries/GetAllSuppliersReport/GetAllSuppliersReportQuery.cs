using MediatR;
using Poliedro.Billing.Application.SuppliersReport.Dtos;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;

namespace Poliedro.Billing.Application.SuppliersReport.Queries.GetAllUtilityReport
{
    public record GetAllSuppliersReportQuery(PaginationParams PaginationParams) : IRequest<Result<IEnumerable<SuppliersReportDto>, Error>>;
    
}
