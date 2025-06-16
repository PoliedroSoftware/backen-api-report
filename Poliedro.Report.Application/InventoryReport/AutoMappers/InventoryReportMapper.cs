using AutoMapper;
using Poliedro.Billing.Application.InventoryReport.Dtos;
using Poliedro.Billing.Domain.InventoryReport.Entities;


namespace Poliedro.Billing.Application.InventoryReport.AutoMappers
{
    public class InventoryReportMapper : Profile
    {
        public InventoryReportMapper()
        {
            CreateMap<InventoryReportEntity, InventoryReportDto>().ReverseMap();
        }
    }
}

