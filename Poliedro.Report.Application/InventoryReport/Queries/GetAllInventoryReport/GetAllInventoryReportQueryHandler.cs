
using AutoMapper;
using MediatR;
using Poliedro.Billing.Application.InventoryReport.Dtos;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Billing.Domain.InventoryReport.DomainInventoryReport;

namespace Poliedro.Billing.Application.InventoryReport.Queries.GetAllInventoryReport;

public class GetAllInventoryReportQueryHandler
(
    IInventoryReportDomainInventoryReport InventoryReportDomainInventoryReport,
    IMapper mapper
) : IRequestHandler<GetAllInventoryReportQuery, Result<IEnumerable<InventoryReportDto>, Error>>
{

    public async Task<Result<IEnumerable<InventoryReportDto>, Error>> 
        Handle(GetAllInventoryReportQuery request, CancellationToken cancellationToken)
    {
        var result = await InventoryReportDomainInventoryReport.GetAllAsync(cancellationToken, request.paginationParams);

        if (!result.IsSuccess && result.Value != null)
            return result.Error!;

        return mapper.Map<List<InventoryReportDto>>(result.Value);
    }
}
