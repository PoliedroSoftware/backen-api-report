using AutoMapper;
using Poliedro.Report.Domain.UtilityReport.Entity;
using Poliedro.Report.Application.UtilityReport.Dtos;

namespace Poliedro.Report.Application.UtilityReport.AutoMappers;

public class UtilityReportMapper : Profile
{
    public UtilityReportMapper() {

        CreateMap<UtilityReportEntity, UtilityReportDto>()
            .ForMember(dest => dest.ForYear, opt => opt.MapFrom(src => src.ForYear))
            .ForMember(dest => dest.ForMonth, opt => opt.MapFrom(src => src.ForMonth))
            .ForMember(dest => dest.ForDay, opt => opt.MapFrom(src => src.ForDay));

        CreateMap<UtilityReportYearEntity, UtilityReportYearDto>()
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Utility, opt => opt.MapFrom(src => src.Utility));

        CreateMap<UtilityReportMonthEntity, UtilityReportMonthDto>()
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
            .ForMember(dest => dest.Utility, opt => opt.MapFrom(src => src.Utility));

        CreateMap<UtilityReportDayEntity, UtilityReportDayDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
            .ForMember(dest => dest.Utility, opt => opt.MapFrom(src => src.Utility));
    }
}
