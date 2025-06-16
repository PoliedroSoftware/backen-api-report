using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Application.DraftReport.Queries.GetAllDraftReport;
using Poliedro.Billing.Domain.Common.Pagination;

namespace Poliedro.Billing.Api.Controllers.v1.DraftReport;

[Route("api/v1/draft-report")]
[ApiController]
public class DraftReportController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var query = new GetAllDraftReportQuery(paginationParams);
        var result = await mediator.Send(query);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        return Ok(result.Value);
    }
}
