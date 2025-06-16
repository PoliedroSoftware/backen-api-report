using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Application.SuppliersReport.Queries.GetAllUtilityReport;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Billing.Api.Controllers.v1.SuppliersReport;

[Route("api/v1/suppliers-report")]
[ApiController]
public class SuppliersReportController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var query = new GetAllSuppliersReportQuery(paginationParams);
        var result = await mediator.Send(query);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        return Ok(result.Value);
    }
}
