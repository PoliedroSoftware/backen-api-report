using MediatR;
using Poliedro.Billing.Domain.Common.Pagination;
using Poliedro.Billing.Domain.Common.Results;
using Poliedro.Billing.Domain.Common.Results.Errors;
using Poliedro.Report.Application.UtilityReport.Dtos;


namespace Poliedro.Report.Application.UtilityReport.Queries.GetAllUtilityReport;

public record GetAllUtilityReportQuery(PaginationParams PaginationParams) : IRequest<Result<UtilityReportDto, Error>>
{
   
}
