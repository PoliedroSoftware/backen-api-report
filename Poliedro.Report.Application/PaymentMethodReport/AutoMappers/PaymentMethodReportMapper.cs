using AutoMapper;
using Poliedro.Report.Application.PaymentMethodReport.Dtos;
using Poliedro.Report.Domain.PaymentMethodReport.Entity;

namespace Poliedro.Report.Application.PaymentMethodReport.AutoMappers
{
    public class PaymentMethodReportMapper : Profile
    {
        public PaymentMethodReportMapper()
        {
            
            CreateMap<PaymentMethodReportEntity, PaymentMethodReportDto>()
                .ForMember(dest => dest.ForYear, opt => opt.MapFrom(src => src.ForYear))
                .ForMember(dest => dest.ForMonth, opt => opt.MapFrom(src => src.ForMonth))
                .ForMember(dest => dest.ForDay, opt => opt.MapFrom(src => src.ForDay));

            
            CreateMap<PaymentMethodReportYearEntity, PaymentMethodReportYearDto>()
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.TotalPaid, opt => opt.MapFrom(src => src.TotalPaid))
                .ForMember(dest => dest.TotalSales, opt => opt.MapFrom(src => src.TotalSales))
                .ForMember(dest => dest.InvoiceQuantity, opt => opt.MapFrom(src => src.InvoiceQuantity));

            
            CreateMap<PaymentMethodReportMonthEntity, PaymentMethodReportMonthDto>()
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Nmonth, opt => opt.MapFrom(src => src.Nmonth))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.TotalPaid, opt => opt.MapFrom(src => src.TotalPaid))
                .ForMember(dest => dest.TotalSales, opt => opt.MapFrom(src => src.TotalSales))
                .ForMember(dest => dest.InvoiceQuantity, opt => opt.MapFrom(src => src.InvoiceQuantity));

            
            CreateMap<PaymentMethodReportDayEntity, PaymentMethodReportDayDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.TotalPaid, opt => opt.MapFrom(src => src.TotalPaid))
                .ForMember(dest => dest.TotalSales, opt => opt.MapFrom(src => src.TotalSales))
                .ForMember(dest => dest.InvoiceQuantity, opt => opt.MapFrom(src => src.InvoiceQuantity))
                .ForMember(dest => dest.TotalSalesDay, opt => opt.MapFrom(src => src.TotalSalesDay));
        }
    }
}
