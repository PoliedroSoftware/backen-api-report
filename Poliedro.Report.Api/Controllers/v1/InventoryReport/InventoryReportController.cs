using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Application.InventoryReport.Queries.GetAllInventoryReport;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Billing.Api.Controllers.v1.InventoryReport;

[Route("api/v1/Inventory-Report")]
[ApiController]
public class InventoryReportController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var query = new GetAllInventoryReportQuery(paginationParams);
        var result = await mediator.Send(query);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        return Ok(result.Value);
    }
}

