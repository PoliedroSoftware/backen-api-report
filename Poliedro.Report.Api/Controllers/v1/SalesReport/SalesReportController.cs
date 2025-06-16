using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Report.Application.SalesReport.Queries.GetAllSalesReport;

namespace Poliedro.Report.Api.Controllers.v1.SalesReport
{
    [Route("api/v1/sales-report")]
    [ApiController]
    public class SalesReportController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var query = new GetAllSalesReportQuery(paginationParams);
            var result = await mediator.Send(query);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }

    }
}
