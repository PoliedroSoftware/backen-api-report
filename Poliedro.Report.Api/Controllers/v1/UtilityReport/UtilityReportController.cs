using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Report.Application.UtilityReport.Queries.GetAllUtilityReport;


namespace Poliedro.Report.Api.Controllers.v1.UtilityReport
{
    [Route("api/v1/utility-report")]
    [ApiController]
    public class UtilityReportController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var query = new GetAllUtilityReportQuery(paginationParams);
            var result = await mediator.Send(query);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }
    }
}
