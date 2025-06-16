using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poliedro.Billing.Domain.InventoryReport.DomainInventoryReport;
using Poliedro.Billing.Domain.DraftReport.DomainDraftReport;
using Poliedro.Billing.Domain.Ports;
using Poliedro.Billing.Domain.SuppliersReport.DomainSuppliersReport;
using Poliedro.Report.Domain.PaymentMethodReport.Ports;
using Poliedro.Report.Domain.InvoiceReport.Ports;
using Poliedro.Report.Domain.UtilityReport.Ports;
using Poliedro.Report.Domain.SalesReport.Ports;
using Poliedro.Report.Application.Ports.Redis;
using Poliedro.Report.Infraestructure.Persistence.Mysql.Adapter;
using Poliedro.Report.Infraestructure.Persistence.Mysql.Context;
using Poliedro.Report.Infraestructure.Persistence.Mysql.InventoryReport.Impl;
using Poliedro.Billing.Infraestructure.Persistence.Mysql.SuppliersReport.DomainService.Impl;
using Poliedro.Report.Infraestructure.Persistence.Mysql.PaymentMethodReport.Impl;
using Poliedro.Report.Infraestructure.Persistence.Mysql.InvoiceReport.Impl;
using Poliedro.Report.Infraestructure.Persistence.Mysql.UtilityReport.ImpI;
using Poliedro.Report.Infraestructure.Persistence.Mysql.SalesReport.ImpI;
using Poliedro.Report.Infraestructure.Persistence.Mysql.Redis;
using Poliedro.Report.Infraestructure.Persistence.Mysql.DraftReport.Impl;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql;

public static class DependencyInjectionService
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION") ?? configuration.GetConnectionString("MysqlConnection");
        services.AddDbContext<DataBaseContext>(
            options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
        ));
      
        services.AddTransient<IMessageProvider, MessageProvider>();
        services.AddTransient<ISuppliersReportDomainSuppliersReport, SuppliersReportDomainService>();
        services.AddTransient<IInventoryReportDomainInventoryReport, InventoryReportDomainService>();
        services.AddTransient<IPaymentMethodReportDomain, PaymentMethodReportDomainService>();
        services.AddTransient<IInvoiceReportDomainService, InvoiceReportDomainService>();
        services.AddTransient<IUtilityReportDomain, UtilityReportDomainService>();
        services.AddTransient<ISalesReportDomain, SalesReportDomainService>();
        services.AddTransient<IDraftReportDomainDraftReport, DraftReportDomainService>();


        services.AddTransient<IRedisService, RedisCacheService>();
        return services;
    }
}
