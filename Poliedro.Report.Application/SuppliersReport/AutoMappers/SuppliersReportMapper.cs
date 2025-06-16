using AutoMapper;
using Poliedro.Billing.Application.SuppliersReport.Dtos;
using Poliedro.Billing.Domain.SuppliersReport.Entities;


namespace Poliedro.Billing.Application.SuppliersReport.AutoMappers
{
    public class SuppliersReportMapper : Profile
    {
        public SuppliersReportMapper()
        {
            CreateMap<SuppliersReportEntity, SuppliersReportDto>().ReverseMap();
        }
    }
}
