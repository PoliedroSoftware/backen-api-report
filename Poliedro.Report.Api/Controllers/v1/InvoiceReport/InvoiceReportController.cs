using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Report.Application.InvoiceReport.Queries.GetAllInvoiceReport;

namespace Poliedro.Report.Api.Controllers.v1.InvoiceReport
{
    [Route("api/v1/invoice-report")]
    [ApiController]
    public class InvoiceReportController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
        {
            var query = new GetAllInvoiceReportQuery(paginationParams);
            var result = await mediator.Send(query);

            if (!result.IsSuccess || result.Value == null)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }
    }
}
