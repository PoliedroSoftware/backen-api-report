using Microsoft.EntityFrameworkCore;
using Poliedro.Billing.Domain.SuppliersReport.Entities;
using Poliedro.Billing.Domain.InventoryReport.Entities;
using Poliedro.Report.Domain.PaymentMethodReport.Entity;
using Poliedro.Report.Domain.InvoiceReport.Entities;
using Poliedro.Report.Domain.UtilityReport.Entity;
using Poliedro.Report.Domain.SalesReport.Entities;
using Poliedro.Billing.Domain.DraftReport.Entities;

namespace Poliedro.Report.Infraestructure.Persistence.Mysql.Context;

public class DataBaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<InventoryReportEntity> InventoryReportBillingElectronic { get; set; }
    public DbSet<SuppliersReportEntity> SuppliersReport{ get; set; }
    public DbSet<PaymentMethodReportEntity> PaymentMethodReport { get; set; }
    public DbSet<InvoiceReportEntity> InvoiceReport { get; set; }
    public DbSet<UtilityReportEntity> UtilityReport { get; set; }
    public DbSet<SalesReportEntity> SalesReport { get; set; }
    public DbSet<DraftReportEntity> DraftReport { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }

   
}
