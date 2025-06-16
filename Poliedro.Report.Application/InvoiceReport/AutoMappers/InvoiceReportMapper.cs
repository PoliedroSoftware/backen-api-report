


using AutoMapper;
using Poliedro.Report.Application.InvoiceReport.Dtos;
using Poliedro.Report.Domain.InvoiceReport.Entities;

namespace Poliedro.Report.Application.InvoiceReport.AutoMappers
{
    public class InvoiceReportMapper : Profile
    {
        public InvoiceReportMapper()
        {
            CreateMap<InvoiceReportEntity, InvoiceDetailReportDto>();
        }
    }
}
