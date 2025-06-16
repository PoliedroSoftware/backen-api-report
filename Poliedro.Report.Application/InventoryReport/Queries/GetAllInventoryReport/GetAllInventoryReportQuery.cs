using MediatR;
using Poliedro.Billing.Application.InventoryReport.Dtos;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;

namespace Poliedro.Billing.Application.InventoryReport.Queries.GetAllInventoryReport
{
    public record GetAllInventoryReportQuery(PaginationParams paginationParams) : IRequest<Result<IEnumerable<InventoryReportDto>, Error>>;
    
}
