using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Report.Application.PaymentMethodReport.Queries.GetAllPaymentMethodReport;


namespace Poliedro.Report.Api.Controllers.v1.PaymentMethodReport;

[Route("api/v1/payment-method-report")]
[ApiController]
public class PaymentMethodReportController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams)
    {
        var query = new GetAllPaymentMethodReportQuery(paginationParams);
        var result = await mediator.Send(query);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        return Ok(result.Value);
    }
}
