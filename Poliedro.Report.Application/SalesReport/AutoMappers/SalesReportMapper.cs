using AutoMapper;
using Poliedro.Report.Application.SalesReport.Dtos;
using Poliedro.Report.Domain.SalesReport.Entities;


namespace Poliedro.Report.Application.SalesReport.AutoMappers
{
    public class SalesReportMapper : Profile
    {
        public SalesReportMapper()
        {

            CreateMap<SalesReportEntity, SalesReportDto>()
                .ForMember(dest => dest.ForYear, opt => opt.MapFrom(src => src.ForYear))
                .ForMember(dest => dest.ForMonth, opt => opt.MapFrom(src => src.ForMonth))
                .ForMember(dest => dest.ForDay, opt => opt.MapFrom(src => src.ForDay));

            CreateMap<SalesReportYearEntity, SalesReportYearDto>()
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src.Sale));

            CreateMap<SalesReportMonthEntity, SalesReportMonthDto>()
                .ForMember(dest => dest.NumberMonth, opt => opt.MapFrom(src => src.NumberMonth))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src.Sale));

            CreateMap<SalesReportDayEntity, SalesReportDayDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.NumberMonth, opt => opt.MapFrom(src => src.NumberMonth))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Sale, opt => opt.MapFrom(src => src.Sale));

        }
    }
}
