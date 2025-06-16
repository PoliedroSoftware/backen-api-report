using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poliedro.Billing.Application.Common.Behaviors;
using Poliedro.Billing.Application.SuppliersReport.AutoMappers;
using Poliedro.Billing.Application.InventoryReport.AutoMappers;
using Poliedro.Report.Application.PaymentMethodReport.AutoMappers;
using Poliedro.Report.Application.InvoiceReport.AutoMappers;
using Poliedro.Report.Application.UtilityReport.AutoMappers;
using Poliedro.Report.Application.SalesReport.AutoMappers;
using Poliedro.Report.Application.DraftReport.Erros;

using System.Reflection;




namespace Poliedro.Report.Application;

public static class DependencyInjectionService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region Mappers
        var mapper = new MapperConfiguration(config =>
        {
            
            config.AddProfile(new InventoryReportMapper());
            config.AddProfile(new SuppliersReportMapper());
            config.AddProfile(new PaymentMethodReportMapper());
            config.AddProfile(new InvoiceReportMapper());
            config.AddProfile(new UtilityReportMapper());
            config.AddProfile(new SalesReportMapper());


        });
        services.AddSingleton(mapper.CreateMapper());
        #endregion

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>)
        );
       

        return services;
    }
}