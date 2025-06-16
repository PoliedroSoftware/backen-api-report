using AutoMapper;
using Poliedro.Billing.Application.DraftReport.Dtos;
using Poliedro.Billing.Domain.DraftReport.Entities;


namespace Poliedro.Billing.Application.DraftReport.AutoMappers
{
    public class DraftReportMapper : Profile
    {
        public DraftReportMapper()
        {
            CreateMap<DraftReportEntity, DraftReportDto>().ReverseMap();
        }
    }
}
